using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TP10.Models;
using TP10.Repository;
using TP10.ViewModels;

namespace TP10.Controllers;

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
        if (!ModelState.IsValid)
        {
            return RedirectToAction("ListarUsuario");
        }
        if (!isLogin())
        {
            return RedirectToAction("Error");
        }
        Usuario usuario = new Usuario(u);
        repositorioUsuario.Create(usuario);
        return RedirectToAction("ListarUsuario");
    }
    // Controlador MODIFICAR
    [HttpGet]
    public IActionResult ModificarUsuario(int idUsuario)
    {
        if (!ModelState.IsValid)
        {
            return RedirectToAction("ListarUsuario");
        }
        if (!isLogin())
        {
            return RedirectToAction("Error");
        }
        Usuario usuarioModificar = repositorioUsuario.GetById(idUsuario);
        return View(new ModificarUsuarioViewModel(usuarioModificar));
    }
    [HttpPost]
    public IActionResult ModificarUsuario(ModificarUsuarioViewModel u)
    {
        if (!ModelState.IsValid)
        {
            return RedirectToAction("ListarUsuario");
        }
        if (!isLogin())
        {
            return RedirectToAction("Error");
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