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
        try
        {
            if (!isLogin())
            {
                return RedirectToRoute(new { controller = "Home", action = "Error" });
            }
            List<Tablero> tableros;
            ListarTableroViewModel listarTablero = new ListarTableroViewModel();
            if (isAdmin())
            {
                tableros = _repositorioTablero.GetAll();
            }
            else
            {
                int usuarioId = HttpContext.Session.GetInt32("Id") ?? -9999;
                tableros = _repositorioTablero.GetByIdUsuario(usuarioId);
            }
            return View(listarTablero.convertirLista(tableros));
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error al intentar listar los tableros {ex.ToString()}");
            return RedirectToRoute(new { controller = "Home", action = "Error" });
        }
    }
    // Controlador CREAR
    [HttpGet]
    public IActionResult CrearTablero()
    {
        try
        {
            if (!isLogin())
            {
                return RedirectToRoute(new { controller = "Home", action = "Error" });
            }
            return View(new CrearTableroViewModel());
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error al intentar crear un tablero {ex.ToString()}");
            return RedirectToRoute(new { controller = "Home", action = "Error" });
        }
    }
    [HttpPost]
    public IActionResult CrearTablero(CrearTableroViewModel t)
    {
        try
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
        catch (Exception ex)
        {
            _logger.LogError($"Error al intentar crear un tablero {ex.ToString()}");
            return RedirectToRoute(new { controller = "Home", action = "Error" });
        }
    }
    // Controlador MODIFICAR
    [HttpGet]
    public IActionResult ModificarTablero(int idTablero)
    {
        try
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
        catch (Exception ex)
        {
            _logger.LogError($"Error al intentar modificar un tablero {ex.ToString()}");
            return RedirectToRoute(new { controller = "Home", action = "Error" });
        }
    }
    [HttpPost]
    public IActionResult ModificarTablero(ModificarTableroViewModel t)
    {
        try
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
        catch (Exception ex)
        {
            _logger.LogError($"Error al intentar modificar un tablero {ex.ToString()}");
            return RedirectToRoute(new { controller = "Home", action = "Error" });
        }
    }
    // Controlador ELIMINAR 
    public IActionResult EliminarTablero(int idTablero)
    {
        try
        {
            if (!isLogin())
            {
                return RedirectToRoute(new { controller = "Home", action = "Error" });
            }
            _repositorioTablero.Remove(idTablero);
            return RedirectToAction("ListarTablero");
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error al intentar eliminar un tablero {ex.ToString()}");
            return RedirectToRoute(new { controller = "Home", action = "Error" });
        }
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