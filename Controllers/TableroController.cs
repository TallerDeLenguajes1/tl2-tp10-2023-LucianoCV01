using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using tl2_tp10_2023_LucianoCV01.Models;
using tl2_tp10_2023_LucianoCV01.Repository;
using tl2_tp10_2023_LucianoCV01.ViewModels;

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
        ListarTableroViewModel listarTableroViewModel = new(tableros);
        return View(listarTableroViewModel);
    }
    // Controlador CREAR
    [HttpGet]
    public IActionResult CrearTablero()
    {
        CrearTableroViewModel crearTableroViewModel = new();
        return View(crearTableroViewModel);
    }
    [HttpPost]
    public IActionResult CrearTablero(CrearTableroViewModel t)
    {
        Tablero tablero = new(t);
        repositorioTablero.Create(tablero);
        // falta campo idUsuarioPropietario
        return RedirectToAction("ListarTablero");
    }
    // Controlador MODIFICAR
    [HttpGet]
    public IActionResult ModificarTablero(int idTablero)
    {
        Tablero tableroModificar = repositorioTablero.GetById(idTablero);
        ModificarTableroViewModel modificarTableroViewModel = new(tableroModificar);
        return View(modificarTableroViewModel);
    }
    [HttpPost]
    public IActionResult ModificarTablero(ModificarTableroViewModel t)
    {
        Tablero tablero = new(t);
        repositorioTablero.Update(tablero.Id, tablero);
        return RedirectToAction("ListarTablero");
    }
    // Controlador ELIMINAR 
    public IActionResult EliminarTablero(int idTablero)
    {
        repositorioTablero.Delete(idTablero);
        return RedirectToAction("ListarTablero");
    }
}