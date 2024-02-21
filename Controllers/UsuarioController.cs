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
                return RedirectToRoute(new { controller = "Login", action = "Index" });
            }
            int idUsuario = idEnSession();
            Usuario usuario = _repositorioUsuario.GetById(idUsuario);
            List<Usuario> usuarios = new();
            if (isAdmin())
            {
                usuarios = _repositorioUsuario.GetAll();
                Usuario usuarioExistente = usuarios.FirstOrDefault(u => u.Id == usuario.Id)!;

                if (usuarioExistente != null)
                {
                    usuarios.Remove(usuarioExistente);
                }
            }
            usuarios.Insert(0, usuario);
            ListarUsuarioViewModel listarUsuarioViewModel = new(usuarios);
            return View(listarUsuarioViewModel);
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
            CrearUsuarioViewModel crearUsuarioViewModel = new();
            return View(crearUsuarioViewModel);
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
            if (!ModelState.IsValid)
            {
                return RedirectToAction("ListarUsuario");
            }
            Usuario usuario = new(u);
            _repositorioUsuario.Create(usuario);
            if (!isAdmin())
            {
                return RedirectToRoute(new { controller = "Login", action = "Index" });
            }
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
                return RedirectToRoute(new { controller = "Home", action = "Error404" });
            }
            if (!isAdmin() && idEnSession() != idUsuario)
            {
                return RedirectToRoute(new { controller = "Home", action = "Error404" });
            }
            Usuario usuarioModificar = _repositorioUsuario.GetById(idUsuario);
            ModificarUsuarioViewModel modificarUsuarioViewModelnew = new(usuarioModificar);
            return View(modificarUsuarioViewModelnew);
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
                return RedirectToRoute(new { controller = "Home", action = "Error404" });
            }
            if (!ModelState.IsValid)
            {
                return RedirectToAction("ListarUsuario");
            }
            Usuario usuario = new(u);
            _repositorioUsuario.Update(usuario.Id, usuario);
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
                return RedirectToRoute(new { controller = "Home", action = "Error404" });
            }
            if (!isAdmin() && idEnSession() != idUsuario)
            {
                return RedirectToRoute(new { controller = "Home", action = "Error404" });
            }
            _repositorioUsuario.Delete(idUsuario);
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
        return HttpContext.Session != null && HttpContext.Session.GetString("NombreDeUsuario") != null;
    }
    private bool isAdmin()
    {
        return isLogin() && HttpContext.Session.GetString("Rol") == "administrador";
    }
    private int idEnSession()
    {
        int? idUsuarioNullable = HttpContext.Session.GetInt32("Id");
        int idUsuario = idUsuarioNullable ?? -9999;
        return idUsuario;
    }
}