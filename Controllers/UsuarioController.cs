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
        List<Usuario> usuarios = repositorioUsuario.GetAll();
        ListarUsuarioViewModel listarUsuarioViewModel = new(usuarios);
        return View(listarUsuarioViewModel);
    }
    // Controlador CREAR
    [HttpGet]
    public IActionResult CrearUsuario()
    {
        CrearUsuarioViewModel crearUsuarioViewModel = new();
        return View(crearUsuarioViewModel);
    }
    [HttpPost]
    public IActionResult CrearUsuario(CrearUsuarioViewModel u)
    {
        if (!ModelState.IsValid)
        {
            return RedirectToAction("ListarUsuario");
        }
        Usuario usuario = new(u);
        repositorioUsuario.Create(usuario);
        return RedirectToAction("ListarUsuario");
    }
    // Controlador MODIFICAR
    [HttpGet]
    public IActionResult ModificarUsuario(int idUsuario)
    {
        Usuario usuarioModificar = repositorioUsuario.GetById(idUsuario);
        ModificarUsuarioViewModel modificarUsuarioViewModelnew = new(usuarioModificar);
        return View(modificarUsuarioViewModelnew);
    }
    [HttpPost]
    public IActionResult ModificarUsuario(ModificarUsuarioViewModel u)
    {
        if (!ModelState.IsValid)
        {
            return RedirectToAction("ListarUsuario");
        }
        Usuario usuario = new(u);
        repositorioUsuario.Update(usuario.Id, usuario);
        return RedirectToAction("ListarUsuario");
    }
    // Controlador ELIMINAR
    public IActionResult EliminarUsuario(int idUsuario)
    {
        repositorioUsuario.Delete(idUsuario);
        return RedirectToAction("ListarUsuario");
    }
}