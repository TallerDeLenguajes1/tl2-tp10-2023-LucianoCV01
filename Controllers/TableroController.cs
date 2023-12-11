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
        if (!isLogin())
        {
            return RedirectToRoute(new { controller = "Home", action = "Error" });
        }
        if (isAdmin())
        {
            List<Tablero> tableros = _repositorioTablero.GetAll();
            var listarTablero = new ListarTableroViewModel();
            return View(listarTablero.convertirLista(tableros));
        }
        else
        {
            int usuarioId = HttpContext.Session.GetInt32("Id") ?? -9999;
            List<Tablero> tableros = _repositorioTablero.GetByIdUsuario(usuarioId);
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
            return RedirectToRoute(new { controller = "Home", action = "Error" });
        }
        return View(new CrearTableroViewModel());
    }
    [HttpPost]
    public IActionResult CrearTablero(CrearTableroViewModel t)
    {
        if (!isLogin())
        {
            return RedirectToRoute(new { controller = "Home", action = "Error" });
        }
        if (!ModelState.IsValid)
        {
            return RedirectToAction("ListarTablero");
        }
        Tablero tablero = new Tablero(t)
        {
            IdUsuarioPropietario = HttpContext.Session.GetInt32("Id") ?? -9999
        };
        _repositorioTablero.Create(tablero);
        return RedirectToAction("ListarTablero");
    }
    // Controlador MODIFICAR
    [HttpGet]
    public IActionResult ModificarTablero(int idTablero)
    {
        if (!isLogin())
        {
            return RedirectToRoute(new { controller = "Home", action = "Error" });
        }
        if (!ModelState.IsValid)
        {
            return RedirectToAction("ListarTablero");
        }
        Tablero tableroModificar = _repositorioTablero.GetById(idTablero);
        return View(new ModificarTableroViewModel(tableroModificar));
    }
    [HttpPost]
    public IActionResult ModificarTablero(ModificarTableroViewModel t)
    {
        if (!isLogin())
        {
            return RedirectToRoute(new { controller = "Home", action = "Error" });
        }
        if (!ModelState.IsValid)
        {
            return RedirectToAction("ListarTablero");
        }
        Tablero tableroModificado = _repositorioTablero.GetById(t.Id);
        tableroModificado.Nombre = t.Nombre;
        tableroModificado.Descripcion = t.Descripcion;
        _repositorioTablero.Update(t.Id, tableroModificado);
        return RedirectToAction("ListarTablero");
    }
    // Controlador ELIMINAR 
    public IActionResult EliminarTablero(int idTablero)
    {
        if (!isLogin())
        {
            return RedirectToRoute(new { controller = "Home", action = "Error" });
        }
        _repositorioTablero.Remove(idTablero);
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
}