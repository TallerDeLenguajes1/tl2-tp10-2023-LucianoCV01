using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using tl2_tp10_2023_LucianoCV01.Models;
using tl2_tp10_2023_LucianoCV01.Repository;
using tl2_tp10_2023_LucianoCV01.ViewModels;

namespace tl2_tp10_2023_LucianoCV01.Controllers;

public class UsuarioController : Controller
{
    private readonly ILogger<UsuarioController> _logger;
    private IUsuarioRepository repositorioUsuario;

    public UsuarioController(ILogger<UsuarioController> logger)
    {
        _logger = logger;
        repositorioUsuario = new UsuarioRepository();
    }
    // Controlador LISTAR
    [HttpGet]
    public IActionResult ListarUsuario()
    {
        if (!isLogin())
        {
            return RedirectToAction("Error");
        }
        List<Usuario> usuarios = repositorioUsuario.GetAll();
        var listarUsuario = new ListarUsuarioViewModel();
        return View(listarUsuario.convertirLista(usuarios));
    }
    // Controlador CREAR
    [HttpGet]
    public IActionResult CrearUsuario()
    {
        if (!isLogin())
        {
            return RedirectToAction("Error");
        }
        return View(new CrearUsuarioViewModel());
    }
    [HttpPost]
    public IActionResult CrearUsuario(CrearUsuarioViewModel u)
    {
        if (!isLogin())
        {
            return RedirectToAction("Error");
        }
        if (!ModelState.IsValid)
        {
            return RedirectToAction("ListarUsuario");
        }
        Usuario usuario = new Usuario(u);
        repositorioUsuario.Create(usuario);
        return RedirectToAction("ListarUsuario");
    }
    // Controlador MODIFICAR
    [HttpGet]
    public IActionResult ModificarUsuario(int idUsuario)
    {
        if (!isLogin())
        {
            return RedirectToAction("Error");
        }
        if (!ModelState.IsValid)
        {
            return RedirectToAction("ListarUsuario");
        }
        Usuario usuarioModificar = repositorioUsuario.GetById(idUsuario);
        return View(new ModificarUsuarioViewModel(usuarioModificar));
    }
    [HttpPost]
    public IActionResult ModificarUsuario(ModificarUsuarioViewModel u)
    {
        if (!isLogin())
        {
            return RedirectToAction("Error");
        }
        if (!ModelState.IsValid)
        {
            return RedirectToAction("ListarUsuario");
        }
        Usuario usuarioModicado = repositorioUsuario.GetById(u.Id);
        usuarioModicado.NombreDeUsuario = u.NombreDeUsuario;
        usuarioModicado.Contrasenia = u.Contrasenia;
        usuarioModicado.Rol = u.Rol;
        repositorioUsuario.Update(u.Id, usuarioModicado);
        return RedirectToAction("ListarUsuario");
    }
    // Controlador ELIMINAR
    public IActionResult EliminarUsuario(int idUsuario)
    {
        if (!isLogin())
        {
            return RedirectToAction("Error");
        }
        repositorioUsuario.Remove(idUsuario);
        return RedirectToAction("ListarUsuario");
    }

    private bool isLogin()
    {
        if (HttpContext.Session != null && HttpContext.Session.GetString("NombreDeUsuario") != null)
        {
            return true;
        }
        return false;
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}