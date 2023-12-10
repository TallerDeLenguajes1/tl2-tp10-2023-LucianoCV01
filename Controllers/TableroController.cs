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
        if (!isLogin())
        {
            return RedirectToAction("Error");
        }
        if (isAdmin())
        {
            List<Tablero> tableros = repositorioTablero.GetAll();
            var listarTablero = new ListarTableroViewModel();
            return View(listarTablero.convertirLista(tableros));
        }
        else
        {
            int usuarioId = HttpContext.Session.GetInt32("Id") ?? -9999;
            List<Tablero> tableros = repositorioTablero.GetByIdUsuario(usuarioId);
            var listarTablero = new ListarTableroViewModel();
            return View(listarTablero.convertirLista(tableros));
        }
    }
    // Controlador CREAR
    [HttpGet]
    public IActionResult CrearTablero()
    {
        if (!isLogin())
        {
            return RedirectToAction("Error");
        }
        return View(new CrearTableroViewModel());
    }
    [HttpPost]
    public IActionResult CrearTablero(CrearTableroViewModel t)
    {
        if (!isLogin())
        {
            return RedirectToAction("Error");
        }
        if (!ModelState.IsValid)
        {
            return RedirectToAction("ListarTablero");
        }
        Tablero tablero = new Tablero(t)
        {
            IdUsuarioPropietario = HttpContext.Session.GetInt32("Id") ?? -9999
        };
        repositorioTablero.Create(tablero);
        return RedirectToAction("ListarTablero");
    }
    // Controlador MODIFICAR
    [HttpGet]
    public IActionResult ModificarTablero(int idTablero)
    {
        if (!isLogin())
        {
            return RedirectToAction("Error");
        }
        if (!ModelState.IsValid)
        {
            return RedirectToAction("ListarTablero");
        }
        Tablero tableroModificar = repositorioTablero.GetById(idTablero);
        return View(new ModificarTableroViewModel(tableroModificar));
    }
    [HttpPost]
    public IActionResult ModificarTablero(ModificarTableroViewModel t)
    {
        if (!isLogin())
        {
            return RedirectToAction("Error");
        }
        if (!ModelState.IsValid)
        {
            return RedirectToAction("ListarTablero");
        }
        Tablero tableroModificado = repositorioTablero.GetById(t.Id);
        tableroModificado.Nombre = t.Nombre;
        tableroModificado.Descripcion = t.Descripcion;
        repositorioTablero.Update(t.Id, tableroModificado);
        return RedirectToAction("ListarTablero");
    }
    // Controlador ELIMINAR 
    public IActionResult EliminarTablero(int idTablero)
    {
        if (!isLogin())
        {
            return RedirectToAction("Error");
        }
        repositorioTablero.Remove(idTablero);
        return RedirectToAction("ListarTablero");
    }

    private bool isLogin()
    {
        return HttpContext.Session != null && HttpContext.Session.GetString("NombreDeUsuario") != null;
    }

    private bool isAdmin()
    {
        return isLogin() && HttpContext.Session.GetString("Rol") == "administrador";
    }

    // private bool isOperador()
    // {
    //     return isLogin() && HttpContext.Session.GetString("Rol") == "operador";
    // } 

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}