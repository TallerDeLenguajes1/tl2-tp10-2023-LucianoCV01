using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using tl2_tp10_2023_LucianoCV01.Models;
using tl2_tp10_2023_LucianoCV01.Repository;

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
        List<Tarea> tareas = repositorioTarea.GetByIdTablero(2);
        return View(tareas);
    }
    // Controlador CREAR
    [HttpGet]
    public IActionResult CrearTarea()
    {
        return View(new Tarea());
    }
    [HttpPost]
    public IActionResult CrearTarea(Tarea t)
    {
        repositorioTarea.Create(t.IdTablero, t);
        return RedirectToAction("ListarTarea");
    }
    // Controlador MODIFICAR
    [HttpGet]
    public IActionResult ModificarTarea(int idTarea)
    {
        Tarea tareaModificar = repositorioTarea.GetById(idTarea);
        return View(tareaModificar);
    }
    [HttpPost]
    public IActionResult ModificarTarea(Tarea t)
    {
        repositorioTarea.Update(t.Id, t);
        return RedirectToAction("ListarTarea");
    }
    // Controlador ELIMINAR
    public IActionResult EliminarTarea(int idTarea)
    {
        repositorioTarea.Delete(idTarea);
        return RedirectToAction("ListarTarea");
    }
}