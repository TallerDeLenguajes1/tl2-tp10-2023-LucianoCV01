using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using tl2_tp10_2023_LucianoCV01.Models;
using tl2_tp10_2023_LucianoCV01.Repository;

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
        return View(usuarios);
    }
    // Controlador CREAR
    [HttpGet]
    public IActionResult CrearUsuario()
    {
        return View(new Usuario());
    }
    [HttpPost]
    public IActionResult CrearUsuario(Usuario u)
    {
        repositorioUsuario.Create(u);
        return RedirectToAction("ListarUsuario");
    }
    // Controlador MODIFICAR
    [HttpGet]
    public IActionResult ModificarUsuario(int idUsuario)
    {
        Usuario usuarioModificar = repositorioUsuario.GetById(idUsuario);
        return View(usuarioModificar);
    }
    [HttpPost]
    public IActionResult ModificarUsuario(Usuario u)
    {
        repositorioUsuario.Update(u.Id, u);
        return RedirectToAction("ListarUsuario");
    }
    // Controlador ELIMINAR
    public IActionResult EliminarUsuario(int idUsuario)
    {
        repositorioUsuario.Delete(idUsuario);
        return RedirectToAction("ListarUsuario");
    }
}