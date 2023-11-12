using Microsoft.AspNetCore.Mvc;
using tl2_tp10_2023_LucianoCV01.Models;
using EspacioRepositorios;

namespace tl2_tp10_2023_LucianoCV01.Controllers;

public class UsuarioController : Controller
{
    private readonly ILogger<UsuarioController> _logger;
    private IUsuarioRepository manejoDeUsuarios;
    public UsuarioController(ILogger<UsuarioController> logger)
    {
        _logger = logger;
        manejoDeUsuarios = new UsuarioRepository();
    }
    // ● En el controlador de usuarios : Listar, Crear, Modificar y Eliminar Usuarios.
    // Controlador LISTAR
    [HttpGet]
    public IActionResult ListarUsuario()
    {
        return View(manejoDeUsuarios.GetAll());
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
        manejoDeUsuarios.Create(u);
        return RedirectToAction("ListarUsuario");
    }
    // Controlador MODIFICAR ----> pq utilzar post en lugar de put 
    [HttpGet]
    public IActionResult ModificarUsuario(int idUsuario)
    {
        return View(manejoDeUsuarios.GetById(idUsuario));
    }
    [HttpPost]
    public IActionResult ModificarUsuario(Usuario u)
    {
        manejoDeUsuarios.Update(u.Id, u);
        return RedirectToAction("ListarUsuario");
    }
    // Controlador ELIMINAR -------> [] de que tipo es el http o no hace falta indicarlo
    // [HttpDelete]
    public IActionResult EliminarUsuario(int idUsuario){
        manejoDeUsuarios.Remove(idUsuario);
        return RedirectToAction("ListarUsuario");
    }
}