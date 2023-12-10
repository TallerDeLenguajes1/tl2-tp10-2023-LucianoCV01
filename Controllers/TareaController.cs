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
        if (!isLogin())
        {
            return RedirectToAction("Error");
        }
        List<Tarea> tareas = repositorioTarea.GetByIdTablero(idTablero);
        var listarTarea = new ListarTareaViewModel();
        return View(listarTarea.convertirLista(tareas));
    }
    // Controlador CREAR
    [HttpGet]
    public IActionResult CrearTarea()
    {
        if (!isLogin())
        {
            return RedirectToAction("Error");
        }
        return View(new CrearTareaViewModel());
    }
    [HttpPost]
    public IActionResult CrearTarea(CrearTareaViewModel t)
    {
        if (!ModelState.IsValid)
        {
            return RedirectToAction("ListarTarea");
        }
        if (!isLogin())
        {
            return RedirectToAction("Error");
        }
        Tarea tarea = new Tarea(t);
        repositorioTarea.Create(t.IdTablero, tarea);
        return RedirectToAction("ListarTarea");
    }
    // Controlador MODIFICAR
    [HttpGet]
    public IActionResult ModificarTarea(int idTarea)
    {
        if (!isLogin())
        {
            return RedirectToAction("Error");
        }
        if (!ModelState.IsValid)
        {
            return RedirectToAction("ListarTarea");
        }
        Tarea tareaModificar = repositorioTarea.GetById(idTarea);
        return View(new ModificarTareaViewModel(tareaModificar));
    }
    [HttpPost]
    public IActionResult ModificarTarea(ModificarTareaViewModel t)
    {
        if (!isLogin())
        {
            return RedirectToAction("Error");
        }
        if (!ModelState.IsValid)
        {
            return RedirectToAction("ListarTarea");
        }
        Tarea tareaModificada = repositorioTarea.GetById(t.Id);
        tareaModificada.Nombre = t.Nombre;
        tareaModificada.Estado = t.Estado;
        tareaModificada.Descripcion = t.Descripcion;
        tareaModificada.Color = t.Color;
        tareaModificada.IdUsuarioAsignado = t.IdUsuarioAsignado;
        repositorioTarea.Update(t.Id, tareaModificada);
        return RedirectToAction("ListarTarea");
    }
    // Controlador ELIMINAR
    public IActionResult EliminarTarea(int idTarea)
    {
        if (!isLogin())
        {
            return RedirectToAction("Error");
        }
        repositorioTarea.Remove(idTarea);
        return RedirectToAction("ListarTarea");
    }

    private bool isLogin()
    {
        if (HttpContext.Session != null && HttpContext.Session.GetString("NombreDeUsuario") != null)
        {
            return true;
        }
        return false;
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}