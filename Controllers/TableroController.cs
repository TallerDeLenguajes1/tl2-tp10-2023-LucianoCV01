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
        try
        {
            if (!isLogin())
            {
                return RedirectToRoute(new { controller = "Login", action = "Index" });
            }
            int idUsuario = idEnSession();
            List<Tablero> tablerosPropios = _repositorioTablero.GetByIdUsuario(idUsuario);
            List<Tablero> tablerosParticipando = _repositorioTablero.GetByIdUsuarioParticipante(idUsuario);
            List<Tablero> tablerosAjenos = _repositorioTablero.GetByIdUsuarioAjenos(idUsuario);
            ListarTableroViewModel listarTableroViewModel = new(tablerosPropios, tablerosParticipando, tablerosAjenos);
            return View(listarTableroViewModel);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error al intentar listar los tableros {ex.ToString()}");
            return RedirectToRoute(new { controller = "Home", action = "Error" });
        }
    }
    // Controlador CREAR
    [HttpGet]
    public IActionResult CrearTablero()
    {
        try
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
        catch (Exception ex)
        {
            _logger.LogError($"Error al intentar crear un tablero {ex.ToString()}");
            return RedirectToRoute(new { controller = "Home", action = "Error" });
        }
    }
    [HttpPost]
    public IActionResult CrearTablero(CrearTableroViewModel t)
    {
        try
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
        catch (Exception ex)
        {
            _logger.LogError($"Error al intentar crear un tablero {ex.ToString()}");
            return RedirectToRoute(new { controller = "Home", action = "Error" });
        }
    }
    // Controlador MODIFICAR
    [HttpGet]
    public IActionResult ModificarTablero(int idTablero)
    {
        try
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
        catch (Exception ex)
        {
            _logger.LogError($"Error al intentar modificar un tablero {ex.ToString()}");
            return RedirectToRoute(new { controller = "Home", action = "Error" });
        }
    }
    [HttpPost]
    public IActionResult ModificarTablero(ModificarTableroViewModel t)
    {
        try
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
        catch (Exception ex)
        {
            _logger.LogError($"Error al intentar modificar un tablero {ex.ToString()}");
            return RedirectToRoute(new { controller = "Home", action = "Error" });
        }
    }
    // Controlador ELIMINAR 
    public IActionResult EliminarTablero(int idTablero)
    {
        try
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
            _repositorioTablero.DeleteTareas(idTablero);
            _repositorioTablero.Delete(idTablero);
            return RedirectToAction("ListarTablero");
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error al intentar eliminar un tablero {ex.ToString()}");
            return RedirectToRoute(new { controller = "Home", action = "Error" });
        }
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