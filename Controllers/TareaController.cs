using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using tl2_tp10_2023_LucianoCV01.Models;
using tl2_tp10_2023_LucianoCV01.Repository;
using tl2_tp10_2023_LucianoCV01.ViewModels;

namespace tl2_tp10_2023_LucianoCV01.Controllers;

public class TareaController : Controller
{
    private readonly ILogger<TareaController> _logger;
    private ITareaRepository repositorioTarea;

    public TareaController(ILogger<TareaController> logger)
    {
        _logger = logger;
        repositorioTarea = new TareaRepository();
    }
    // Controlador LISTAR 
    [HttpGet]
    public IActionResult ListarTarea(int idTablero)
    {
        List<Tarea> tareas = repositorioTarea.GetByIdTablero(idTablero);
        ListarTareaViewModel listarTareaViewModel = new(tareas);
        return View(listarTareaViewModel);
    }
    // Controlador CREAR
    [HttpGet]
    public IActionResult CrearTarea()
    {
        CrearTareaViewModel crearTareaViewModel = new();
        return View(crearTareaViewModel);
    }
    [HttpPost]
    public IActionResult CrearTarea(CrearTareaViewModel t)
    {
        Tarea tarea = new(t);
        repositorioTarea.Create(tarea.IdTablero, tarea);
        return RedirectToAction("ListarTarea");
    }
    // Controlador MODIFICAR
    [HttpGet]
    public IActionResult ModificarTarea(int idTarea)
    {
        Tarea tareaModificar = repositorioTarea.GetById(idTarea);
        ModificarTareaViewModel modificarTareaViewModel = new(tareaModificar);
        return View(modificarTareaViewModel);
    }
    [HttpPost]
    public IActionResult ModificarTarea(ModificarTareaViewModel t)
    {
        Tarea tarea = new(t);
        repositorioTarea.Update(tarea.Id, tarea);
        return RedirectToAction("ListarTarea");
    }
    // Controlador ELIMINAR
    public IActionResult EliminarTarea(int idTarea)
    {
        repositorioTarea.Delete(idTarea);
        return RedirectToAction("ListarTarea");
    }
}