using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using tl2_tp10_2023_LucianoCV01.Models;
using tl2_tp10_2023_LucianoCV01.Repository;
using tl2_tp10_2023_LucianoCV01.ViewModels;

namespace tl2_tp10_2023_LucianoCV01.Controllers;

public class UsuarioController : Controller
{
    private readonly ILogger<UsuarioController> _logger;
    private IUsuarioRepository _repositorioUsuario;

    public UsuarioController(ILogger<UsuarioController> logger, IUsuarioRepository repositorioUsuario)
    {
        _logger = logger;
        _repositorioUsuario = repositorioUsuario;
    }
    // Controlador LISTAR
    [HttpGet]
    public IActionResult ListarUsuario()
    {
        try
        {
            if (!isLogin())
            {
                return RedirectToRoute(new { controller = "Home", action = "Error" });
            }
            List<Usuario> usuarios = _repositorioUsuario.GetAll();
            var listarUsuario = new ListarUsuarioViewModel();
            return View(listarUsuario.convertirLista(usuarios));
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error al intentar listar los usuarios {ex.ToString()}");
            return RedirectToRoute(new { controller = "Home", action = "Error" });
        }
    }
    // Controlador CREAR
    [HttpGet]
    public IActionResult CrearUsuario()
    {
        try
        {
            if (!isLogin())
            {
                return RedirectToRoute(new { controller = "Home", action = "Error" });
            }
            return View(new CrearUsuarioViewModel());
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error al intentar crear un usuario {ex.ToString()}");
            return RedirectToRoute(new { controller = "Home", action = "Error" });
        }
    }
    [HttpPost]
    public IActionResult CrearUsuario(CrearUsuarioViewModel u)
    {
        try
        {
            if (!isLogin())
            {
                return RedirectToRoute(new { controller = "Home", action = "Error" });
            }
            if (!ModelState.IsValid)
            {
                return RedirectToAction("ListarUsuario");
            }
            Usuario usuario = new Usuario(u);
            _repositorioUsuario.Create(usuario);
            return RedirectToAction("ListarUsuario");
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error al intentar crear un usuario {ex.ToString()}");
            return RedirectToRoute(new { controller = "Home", action = "Error" });
        }
    }
    // Controlador MODIFICAR
    [HttpGet]
    public IActionResult ModificarUsuario(int idUsuario)
    {
        try
        {
            if (!isLogin())
            {
                return RedirectToRoute(new { controller = "Home", action = "Error" });
            }
            if (!ModelState.IsValid)
            {
                return RedirectToAction("ListarUsuario");
            }
            Usuario usuarioModificar = _repositorioUsuario.GetById(idUsuario);
            return View(new ModificarUsuarioViewModel(usuarioModificar));
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error al intentar modificar un usuario {ex.ToString()}");
            return RedirectToRoute(new { controller = "Home", action = "Error" });
        }
    }
    [HttpPost]
    public IActionResult ModificarUsuario(ModificarUsuarioViewModel u)
    {
        try
        {
            if (!isLogin())
            {
                return RedirectToRoute(new { controller = "Home", action = "Error" });
            }
            if (!ModelState.IsValid)
            {
                return RedirectToAction("ListarUsuario");
            }
            Usuario usuarioModicado = _repositorioUsuario.GetById(u.Id);
            usuarioModicado.NombreDeUsuario = u.NombreDeUsuario;
            usuarioModicado.Contrasenia = u.Contrasenia;
            usuarioModicado.Rol = u.Rol;
            _repositorioUsuario.Update(u.Id, usuarioModicado);
            return RedirectToAction("ListarUsuario");
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error al intentar modificar un usuario {ex.ToString()}");
            return RedirectToRoute(new { controller = "Home", action = "Error" });
        }
    }
    // Controlador ELIMINAR
    public IActionResult EliminarUsuario(int idUsuario)
    {
        try
        {
            if (!isLogin())
            {
                return RedirectToRoute(new { controller = "Home", action = "Error" });
            }
            _repositorioUsuario.Remove(idUsuario);
            return RedirectToAction("ListarUsuario");
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error al intentar eliminar un usuario {ex.ToString()}");
            return RedirectToRoute(new { controller = "Home", action = "Error" });
        }
    }

    private bool isLogin()
    {
        if (HttpContext.Session != null && HttpContext.Session.GetString("NombreDeUsuario") != null)
        {
            return true;
        }
        return false;
    }
}