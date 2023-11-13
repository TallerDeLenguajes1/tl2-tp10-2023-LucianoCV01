using Microsoft.AspNetCore.Mvc;
using tl2_tp10_2023_LucianoCV01.Models;
using EspacioRepositorios;

namespace tl2_tp10_2023_LucianoCV01.Controllers;

public class TableroController : Controller
{
    const int idUsuarioPrueba = 1;
    private ITableroRepository manejoDeTableros;
    private readonly ILogger<TableroController> _logger;

    public TableroController(ILogger<TableroController> logger)
    {
        _logger = logger;
        manejoDeTableros = new TableroRepository();
    }
    // En el controlador de tableros: Listar, Crear, Modificar y Eliminar Tableros. (Por el
    // momento asuma que el usuario propietario es siempre el mismo)
     // Controlador LISTAR
    [HttpGet]
    public IActionResult ListarTablero()
    {
        return View(manejoDeTableros.GetByIdUsuario(idUsuarioPrueba));
    }
    // Controlador CREAR
    [HttpGet]
    public IActionResult CrearTablero()
    {
        return View(new Tablero());
    }
    [HttpPost]
    public IActionResult CrearTablero(Tablero t)
    {
        manejoDeTableros.Create(idUsuarioPrueba, t);
        return RedirectToAction("ListarTablero");
    }
    // Controlador MODIFICAR ----> pq utilzar post en lugar de put 
    [HttpGet]
    public IActionResult ModificarTablero(int idTablero)
    {
        return View(manejoDeTableros.GetById(idTablero));
    }
    [HttpPost]
    public IActionResult ModificarTablero(Tablero t)
    {
        manejoDeTableros.Update(t.Id, t);
        return RedirectToAction("ListarTablero");
    }
    // Controlador ELIMINAR -------> [] de que tipo es el http o no hace falta indicarlo
    // [HttpDelete]
    public IActionResult EliminarTablero(int idTablero){
        manejoDeTableros.Remove(idTablero);
        return RedirectToAction("ListarTablero");
    }
}