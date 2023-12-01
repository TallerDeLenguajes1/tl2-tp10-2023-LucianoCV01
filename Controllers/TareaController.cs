using Microsoft.AspNetCore.Mvc;
using tl2_tp10_2023_LucianoCV01.Models;
using EspacioRepositorios;

namespace tl2_tp10_2023_LucianoCV01.Controllers;

public class TareaController : Controller
{
    const int idTableroPrueba = 1;
    private ITareaRepository manejoDeTareas;
    private readonly ILogger<TareaController> _logger;

    public TareaController(ILogger<TareaController> logger)
    {
        _logger = logger;
        manejoDeTareas = new TareaRepository();
    }
    // En el controlador de tareas: Listar, Crear, Modificar y Eliminar Tareas. (Por el
    // momento asuma que el tablero al que pertenece la tarea es siempre la misma, y que 
    // no posee usuario asignado)
    // Controlador LISTAR 
    [HttpGet]
    public IActionResult ListarTarea()
    {
        if (isAdmin())
        {
            return View(manejoDeTareas.GetAll()); // Hacer get all tareas
        }
        else
        {
            if (HttpContext.Session.GetString("Rol") == "operador") // Cambiar la funcion isAdmin por getRol
            {
                return View(manejoDeTareas.GetByIdUsuario(Int32.Parse(HttpContext.Session.GetString("Id")!)));
            }
            else
            {
                return View();
                // PONER EN EL ERROR 
            }
        }
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
        manejoDeTareas.Create(idTableroPrueba, t);
        return RedirectToAction("ListarTarea");
    }
    // Controlador MODIFICAR ----> pq utilzar post en lugar de put 
    [HttpGet]
    public IActionResult ModificarTarea(int idTarea)
    {
        return View(manejoDeTareas.GetById(idTarea));
    }
    [HttpPost]
    public IActionResult ModificarTarea(Tarea t)
    {
        manejoDeTareas.Update(t.Id, t);
        return RedirectToAction("ListarTarea");
    }
    // Controlador ELIMINAR -------> [] de que tipo es el http o no hace falta indicarlo
    // [HttpDelete]
    public IActionResult EliminarTarea(int idTarea)
    {
        manejoDeTareas.Remove(idTarea);
        return RedirectToAction("ListarTarea");
    }

    private bool isAdmin()
    {
        if (HttpContext.Session != null && HttpContext.Session.GetString("Rol") == "admin")
            return true;

        return false;
    }
}