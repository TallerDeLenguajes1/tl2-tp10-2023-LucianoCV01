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
        try
        {
            if (!isLogin())
            {
                return RedirectToRoute(new { controller = "Home", action = "Error" });
            }
            List<Tarea> tareas = _repositorioTarea.GetByIdTablero(idTablero);
            var listarTarea = new ListarTareaViewModel();
            return View(listarTarea.convertirLista(tareas));
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error al intentar listar las tareas {ex.ToString()}");
            return RedirectToRoute(new { controller = "Home", action = "Error" });
        }
    }
    // Controlador CREAR
    [HttpGet]
    public IActionResult CrearTarea()
    {
        try
        {
            if (!isLogin())
            {
                return RedirectToRoute(new { controller = "Home", action = "Error" });
            }
            return View(new CrearTareaViewModel());
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error al intentar crear una tarea {ex.ToString()}");
            return RedirectToRoute(new { controller = "Home", action = "Error" });
        }
    }
    [HttpPost]
    public IActionResult CrearTarea(CrearTareaViewModel t)
    {
        try
        {
            if (!isLogin())
            {
                return RedirectToRoute(new { controller = "Home", action = "Error" });
            }
            if (!ModelState.IsValid)
            {
                return RedirectToAction("ListarTarea");
            }
            Tarea tarea = new Tarea(t);
            _repositorioTarea.Create(t.IdTablero, tarea);
            return RedirectToAction("ListarTarea");
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error al intentar crear una tarea {ex.ToString()}");
            return RedirectToRoute(new { controller = "Home", action = "Error" });
        }
    }
    // Controlador MODIFICAR
    [HttpGet]
    public IActionResult ModificarTarea(int idTarea)
    {
        try
        {
            if (!isLogin())
            {
                return RedirectToRoute(new { controller = "Home", action = "Error" });
            }
            if (!ModelState.IsValid)
            {
                return RedirectToAction("ListarTarea");
            }
            Tarea tareaModificar = _repositorioTarea.GetById(idTarea);
            return View(new ModificarTareaViewModel(tareaModificar));
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error al intentar modificar una tarea {ex.ToString()}");
            return RedirectToRoute(new { controller = "Home", action = "Error" });
        }
    }
    [HttpPost]
    public IActionResult ModificarTarea(ModificarTareaViewModel t)
    {
        try
        {
            if (!isLogin())
            {
                return RedirectToRoute(new { controller = "Home", action = "Error" });
            }
            if (!ModelState.IsValid)
            {
                return RedirectToAction("ListarTarea");
            }
            Tarea tareaModificada = _repositorioTarea.GetById(t.Id);
            tareaModificada.Nombre = t.Nombre;
            tareaModificada.Estado = t.Estado;
            tareaModificada.Descripcion = t.Descripcion;
            tareaModificada.Color = t.Color;
            tareaModificada.IdUsuarioAsignado = t.IdUsuarioAsignado;
            _repositorioTarea.Update(t.Id, tareaModificada);
            return RedirectToAction("ListarTarea");
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error al intentar modificar una tarea {ex.ToString()}");
            return RedirectToRoute(new { controller = "Home", action = "Error" });
        }
    }
    // Controlador ELIMINAR
    public IActionResult EliminarTarea(int idTarea)
    {
        try
        {
            if (!isLogin())
            {
                return RedirectToRoute(new { controller = "Home", action = "Error" });
            }
            _repositorioTarea.Remove(idTarea);
            return RedirectToAction("ListarTarea");
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error al intentar eliminar una tarea {ex.ToString()}");
            return RedirectToRoute(new { controller = "Home", action = "Error" });
        }
    }

    private bool isLogin()
    {
        if (HttpContext.Session != null && HttpContext.Session.GetString("NombreDeUsuario") != null)
        {
            return true;
        }
        return false;
    }
}