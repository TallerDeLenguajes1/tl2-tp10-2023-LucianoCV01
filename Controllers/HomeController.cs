using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using tl2_tp10_2023_LucianoCV01.Models;
using tl2_tp10_2023_LucianoCV01.Repository;

namespace tl2_tp10_2023_LucianoCV01.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
        // ITableroRepository repo = new TableroRepository();
        // repo.Delete(1);
        // var tab = new Tablero();
        // tab.Nombre = "Prueba";
        // tab.IdUsuarioPropietario = 3;
        // repo.Create(tab);
        // var tableros = repo.GetById(2);
        // var usuprop = repo.GetByIdUsuario(3);
        // // tableros.Descripcion = "Hola Mundo";
        // // repo.Update(tableros.Id, tableros);
        // tableros.Nombre = "Pepito";
        // repo.Update(tableros.Id, tableros);
        // var tableros2 = repo.GetAll();
        // repo.Delete(tableros.Id);
        // tableros2 = repo.GetAll();

    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
