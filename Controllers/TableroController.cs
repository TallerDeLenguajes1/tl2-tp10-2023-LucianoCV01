using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using tl2_tp10_2023_LucianoCV01.Models;
using tl2_tp10_2023_LucianoCV01.Repository;

namespace tl2_tp10_2023_LucianoCV01.Controllers;

public class TableroController : Controller
{
    private readonly ILogger<TableroController> _logger;
    private ITableroRepository repositorioTablero;

    public TableroController(ILogger<TableroController> logger)
    {
        _logger = logger;
        repositorioTablero = new TableroRepository();
    }
    // Controlador LISTAR
    [HttpGet]
    public IActionResult ListarTablero()
    {
        List<Tablero> tableros = repositorioTablero.GetAll();
        return View(tableros);
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
        repositorioTablero.Create(t);
        return RedirectToAction("ListarTablero");
    }
    // Controlador MODIFICAR
    [HttpGet]
    public IActionResult ModificarTablero(int idTablero)
    {
        Tablero tableroModificar = repositorioTablero.GetById(idTablero);
        return View(tableroModificar);
    }
    [HttpPost]
    public IActionResult ModificarTablero(Tablero t)
    {
        repositorioTablero.Update(t.Id, t);
        return RedirectToAction("ListarTablero");
    }
    // Controlador ELIMINAR 
    public IActionResult EliminarTablero(int idTablero)
    {
        repositorioTablero.Delete(idTablero);
        return RedirectToAction("ListarTablero");
    }
}