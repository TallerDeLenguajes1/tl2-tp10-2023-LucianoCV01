using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using tl2_tp10_2023_LucianoCV01.Models;
using tl2_tp10_2023_LucianoCV01.Repository;
using tl2_tp10_2023_LucianoCV01.ViewModels;

namespace tl2_tp10_2023_LucianoCV01.Controllers;

public class TareaController : Controller
{
    private readonly ILogger<TareaController> _logger;
    private ITareaRepository _repositorioTarea;
    private ITableroRepository _repositorioTablero;
    private IUsuarioRepository _repositorioUsuario;

    public TareaController(ILogger<TareaController> logger, ITareaRepository repositorioTarea, ITableroRepository repositorioTablero, IUsuarioRepository repositorioUsuario)
    {
        _logger = logger;
        _repositorioTarea = repositorioTarea;
        _repositorioTablero = repositorioTablero;
        _repositorioUsuario = repositorioUsuario;
    }
    // Controlador LISTAR 
    [HttpGet]
    public IActionResult ListarTarea(int idTablero)
    {
        if (!isLogin())
        {
            return RedirectToRoute(new { controller = "Home", action = "Error404" });
        }
        if (!isAdmin())
        {
            List<Usuario> participantes = _repositorioTablero.GetByIdTableroSusParticipantes(idTablero);
            int idUsuario = idEnSession();
            var usuarioEncontrado = participantes.FirstOrDefault(u => u.Id == idUsuario);

            if (usuarioEncontrado == null)
            {
                return RedirectToRoute(new { controller = "Home", action = "Error404" });
            }
        }
        int tableroDuenio = _repositorioTablero.GetById(idTablero).IdUsuarioPropietario;
        List<Tarea> tareas = _repositorioTarea.GetByIdTablero(idTablero);
        List<Usuario> usuarios = _repositorioUsuario.GetAll();
        ListarTareaViewModel listarTareaViewModel = new(idTablero, tableroDuenio, usuarios, tareas);
        return View(listarTareaViewModel);
    }
    // Controlador CREAR
    [HttpGet]
    public IActionResult CrearTarea(int idTablero)
    {
        if (!isLogin())
        {
            return RedirectToRoute(new { controller = "Home", action = "Error404" });
        }
        CrearTareaViewModel crearTareaViewModel = new();
        crearTareaViewModel.IdTablero = idTablero;

        List<Usuario> usuarios = _repositorioUsuario.GetAll();
        ListarUsuarioViewModel usuariosDisponibles = new(usuarios);
        crearTareaViewModel.UsuariosDisponibles = usuariosDisponibles.Usuarios;
        return View(crearTareaViewModel);
    }
    [HttpPost]
    public IActionResult CrearTarea(CrearTareaViewModel t)
    {
        if (!isLogin())
        {
            return RedirectToRoute(new { controller = "Home", action = "Error404" });
        }
        if (!ModelState.IsValid)
        {
            return RedirectToAction("ListarTarea");
        }
        Tarea tarea = new(t);
        _repositorioTarea.Create(tarea.IdTablero, tarea);
        return RedirectToAction("ListarTarea", new { idTablero = tarea.IdTablero });
    }
    // Controlador MODIFICAR
    [HttpGet]
    public IActionResult ModificarTarea(int idTarea)
    {
        if (!isLogin())
        {
            return RedirectToRoute(new { controller = "Home", action = "Error404" });
        }
        Tarea tareaModificar = _repositorioTarea.GetById(idTarea);
        ModificarTareaViewModel modificarTareaViewModel = new(tareaModificar);
        int tableroDuenio = _repositorioTablero.GetById(tareaModificar.IdTablero).IdUsuarioPropietario;
        modificarTareaViewModel.IdDuenio = tableroDuenio;
        if (!isAdmin() && idEnSession() != tableroDuenio)
        {
            return RedirectToRoute(new { controller = "Home", action = "Error404" });
        }

        List<Usuario> usuarios = _repositorioUsuario.GetAll();
        ListarUsuarioViewModel usuariosDisponibles = new(usuarios);
        modificarTareaViewModel.UsuariosDisponibles = usuariosDisponibles.Usuarios;

        return View(modificarTareaViewModel);
    }
    [HttpPost]
    public IActionResult ModificarTarea(ModificarTareaViewModel t)
    {
        if (!isLogin())
        {
            return RedirectToRoute(new { controller = "Home", action = "Error404" });
        }
        if (!ModelState.IsValid)
        {
            return RedirectToAction("ListarTarea");
        }
        Tarea tarea = new(t);
        _repositorioTarea.Update(tarea.Id, tarea);
        return RedirectToAction("ListarTarea", new { idTablero = tarea.IdTablero });
    }
    // Controlador ELIMINAR
    public IActionResult EliminarTarea(int idTarea)
    {
        if (!isLogin())
        {
            return RedirectToRoute(new { controller = "Home", action = "Error404" });
        }
        Tarea tarea = _repositorioTarea.GetById(idTarea);
        Tablero tablero = _repositorioTablero.GetById(tarea.IdTablero);
        if (!isAdmin() && idEnSession() != tablero.IdUsuarioPropietario)
        {
            return RedirectToRoute(new { controller = "Home", action = "Error404" });
        }
        int idTablero = _repositorioTarea.GetById(idTarea).IdTablero;
        _repositorioTarea.Delete(idTarea);
        return RedirectToAction("ListarTarea", new { idTablero });
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