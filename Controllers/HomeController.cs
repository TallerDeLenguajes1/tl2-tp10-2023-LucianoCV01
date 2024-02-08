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
        // IUsuarioRepository repo = new UsuarioRepository();
        // var usu = new Usuario();
        // usu.Id = 2;
        // usu.NombreDeUsuario = "Luciano";
        // repo.Create(usu);
        // var usuarios = repo.GetAll();
        // usu.NombreDeUsuario = "Luciano Cosentino";
        // repo.Update(usu);
        // var usua = repo.GetById(1);
        // repo.Delete(usua.Id);
        // var usuarios2 = repo.GetAll();
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
