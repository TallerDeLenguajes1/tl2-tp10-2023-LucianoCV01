using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using tl2_tp10_2023_LucianoCV01.Models;
using tl2_tp10_2023_LucianoCV01.Repository;
using tl2_tp10_2023_LucianoCV01.ViewModels;

namespace tl2_tp10_2023_LucianoCV01.Controllers;

public class TableroController : Controller
{
    private readonly ILogger<TableroController> _logger;
    private ITableroRepository _repositorioTablero;

    public TableroController(ILogger<TableroController> logger, ITableroRepository repositorioTablero)
    {
        _logger = logger;
        _repositorioTablero = repositorioTablero;
    }
    // Controlador LISTAR
    [HttpGet]
    public IActionResult ListarTablero()
    {
        List<Tablero> tableros = _repositorioTablero.GetAll();
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
        if (!ModelState.IsValid)
        {
            return RedirectToAction("ListarTablero");
        }
        Tablero tablero = new(t);
        _repositorioTablero.Create(tablero);
        // falta campo idUsuarioPropietario
        return RedirectToAction("ListarTablero");
    }
    // Controlador MODIFICAR
    [HttpGet]
    public IActionResult ModificarTablero(int idTablero)
    {
        Tablero tableroModificar = _repositorioTablero.GetById(idTablero);
        ModificarTableroViewModel modificarTableroViewModel = new(tableroModificar);
        return View(modificarTableroViewModel);
    }
    [HttpPost]
    public IActionResult ModificarTablero(ModificarTableroViewModel t)
    {
        if (!ModelState.IsValid)
        {
            return RedirectToAction("ListarTablero");
        }
        Tablero tablero = new(t);
        _repositorioTablero.Update(tablero.Id, tablero);
        return RedirectToAction("ListarTablero");
    }
    // Controlador ELIMINAR 
    public IActionResult EliminarTablero(int idTablero)
    {
        _repositorioTablero.Delete(idTablero);
        return RedirectToAction("ListarTablero");
    }
}