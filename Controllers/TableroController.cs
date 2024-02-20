using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using tl2_tp10_2023_LucianoCV01.Models;
using tl2_tp10_2023_LucianoCV01.Repository;
using tl2_tp10_2023_LucianoCV01.ViewModels;

namespace tl2_tp10_2023_LucianoCV01.Controllers;

public class TableroController : Controller
{
    private readonly ILogger<TableroController> _logger;
    private ITableroRepository _repositorioTablero;
    private IUsuarioRepository _repositorioUsuario;

    public TableroController(ILogger<TableroController> logger, ITableroRepository repositorioTablero, IUsuarioRepository repositorioUsuario)
    {
        _logger = logger;
        _repositorioTablero = repositorioTablero;
        _repositorioUsuario = repositorioUsuario;
    }
    // Controlador LISTAR
    [HttpGet]
    public IActionResult ListarTablero()
    {
        if (!isLogin())
        {
            return RedirectToRoute(new { controller = "Home", action = "Error404" });
        }
        int idUsuario = idEnSession();
        List<Tablero> tablerosPropios = _repositorioTablero.GetByIdUsuario(idUsuario);
        List<Tablero> tablerosParticipando = _repositorioTablero.GetByIdUsuarioParticipante(idUsuario);
        List<Tablero> tablerosAjenos = _repositorioTablero.GetByIdUsuarioAjenos(idUsuario);
        ListarTableroViewModel listarTableroViewModel = new(tablerosPropios, tablerosParticipando, tablerosAjenos);
        return View(listarTableroViewModel);
    }
    // Controlador CREAR
    [HttpGet]
    public IActionResult CrearTablero()
    {
        if (!isLogin())
        {
            return RedirectToRoute(new { controller = "Home", action = "Error404" });
        }
        CrearTableroViewModel crearTableroViewModel = new();
        List<Usuario> usuarios = _repositorioUsuario.GetAll();
        ListarUsuarioViewModel usuariosDisponibles = new(usuarios);
        UsuarioViewModel usuarioSession = usuariosDisponibles.Usuarios.FirstOrDefault(u => u.Id == idEnSession())!;
        if (usuarioSession != null)
        {
            usuariosDisponibles.Usuarios.Remove(usuarioSession);
            usuariosDisponibles.Usuarios.Insert(0, usuarioSession);
        }
        crearTableroViewModel.UsuariosDisponibles = usuariosDisponibles.Usuarios;
        return View(crearTableroViewModel);
    }
    [HttpPost]
    public IActionResult CrearTablero(CrearTableroViewModel t)
    {
        if (!isLogin())
        {
            return RedirectToRoute(new { controller = "Home", action = "Error404" });
        }
        if (!ModelState.IsValid)
        {
            return RedirectToAction("ListarTablero");
        }
        Tablero tablero = new(t);
        _repositorioTablero.Create(tablero);
        return RedirectToAction("ListarTablero");
    }
    // Controlador MODIFICAR
    [HttpGet]
    public IActionResult ModificarTablero(int idTablero)
    {
        if (!isLogin())
        {
            return RedirectToRoute(new { controller = "Home", action = "Error404" });
        }
        Tablero tableroModificar = _repositorioTablero.GetById(idTablero);
        if (!isAdmin() && idEnSession() != tableroModificar.IdUsuarioPropietario)
        {
            return RedirectToRoute(new { controller = "Home", action = "Error404" });
        }
        ModificarTableroViewModel modificarTableroViewModel = new(tableroModificar);

        List<Usuario> usuarios = _repositorioUsuario.GetAll();
        ListarUsuarioViewModel usuariosDisponibles = new(usuarios);
        UsuarioViewModel usuarioSession = usuariosDisponibles.Usuarios.FirstOrDefault(u => u.Id == idEnSession())!;
        if (usuarioSession != null)
        {
            usuariosDisponibles.Usuarios.Remove(usuarioSession);
            usuariosDisponibles.Usuarios.Insert(0, usuarioSession);
        }
        modificarTableroViewModel.UsuariosDisponibles = usuariosDisponibles.Usuarios;

        return View(modificarTableroViewModel);
    }
    [HttpPost]
    public IActionResult ModificarTablero(ModificarTableroViewModel t)
    {
        if (!isLogin())
        {
            return RedirectToRoute(new { controller = "Home", action = "Error404" });
        }
        if (!ModelState.IsValid)
        {
            return RedirectToAction("ListarTablero");
        }
        Tablero tablero = new(t);
        _repositorioTablero.Update(tablero.Id, tablero);
        return RedirectToAction("ListarTablero");
    }
    // Controlador ELIMINAR 
    public IActionResult EliminarTablero(int idTablero)
    {
        if (!isLogin())
        {
            return RedirectToRoute(new { controller = "Home", action = "Error404" });
        }
        Tablero tablero = _repositorioTablero.GetById(idTablero);
        if (!isAdmin() && idEnSession() != tablero.IdUsuarioPropietario)
        {
            return RedirectToRoute(new { controller = "Home", action = "Error404" });
        }
        _repositorioTablero.Delete(idTablero);
        return RedirectToAction("ListarTablero");
    }

    private bool isLogin()
    {
        return HttpContext.Session != null && HttpContext.Session.GetString("NombreDeUsuario") != null;
    }
    private bool isAdmin()
    {
        return isLogin() && HttpContext.Session.GetString("Rol") == "administrador";
    }
    private int idEnSession()
    {
        int? idUsuarioNullable = HttpContext.Session.GetInt32("Id");
        int idUsuario = idUsuarioNullable ?? -9999;
        return idUsuario;
    }
}