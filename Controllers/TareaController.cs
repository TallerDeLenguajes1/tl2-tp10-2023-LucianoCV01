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

    public TareaController(ILogger<TareaController> logger, ITareaRepository repositorioTarea)
    {
        _logger = logger;
        _repositorioTarea = repositorioTarea;
    }
    // Controlador LISTAR 
    [HttpGet]
    public IActionResult ListarTarea(int idTablero)
    {
        if (!isLogin())
        {
            return RedirectToRoute(new { controller = "Home", action = "Error404" });
        }
        List<Tarea> tareas = _repositorioTarea.GetByIdTablero(idTablero);
        ListarTareaViewModel listarTareaViewModel = new(idTablero, tareas);
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
}