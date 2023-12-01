using Microsoft.AspNetCore.Mvc;
using tl2_tp10_2023_LucianoCV01.Models;
using tl2_tp10_2023_LucianoCV01.ViewModels;
using EspacioRepositorios;

namespace tl2_tp10_2023_LucianoCV01.Controllers;

public class LoginController : Controller
{
    private readonly ILogger<LoginController> _logger;
    private IUsuarioRepository manejoDeUsuarios;
    public LoginController(ILogger<LoginController> logger)
    {
        _logger = logger;
        manejoDeUsuarios = new UsuarioRepository();
    }
    public IActionResult Index()
    {
        return View(new LoginViewModel());
    }

    public IActionResult Login(Usuario usuario)
    {
        var usuarioLogeado =  manejoDeUsuarios.GetAll().FirstOrDefault(u => u.NombreDeUsuario == usuario.NombreDeUsuario && u.Contrasenia == usuario.Contrasenia);

        if (usuarioLogeado == null) return RedirectToAction("Index");

        logearUsuario(usuarioLogeado);

        return RedirectToRoute(new { controller = "Home", action = "Index" });
    }

    private void logearUsuario(Usuario user)
    {
        HttpContext.Session.SetString("Id", user.Id.ToString());
        HttpContext.Session.SetString("Usuario", user.NombreDeUsuario!);
        HttpContext.Session.SetString("Rol", user.Rol.ToString());
    }
}