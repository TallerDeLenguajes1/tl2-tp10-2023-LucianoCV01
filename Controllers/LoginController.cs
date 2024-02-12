using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using tl2_tp10_2023_LucianoCV01.Models;
using tl2_tp10_2023_LucianoCV01.Repository;
using tl2_tp10_2023_LucianoCV01.ViewModels;

namespace tl2_tp10_2023_LucianoCV01.Controllers;

public class LoginController : Controller
{
    private readonly ILogger<LoginController> _logger;
    private IUsuarioRepository _repositorioUsuario;

    public LoginController(ILogger<LoginController> logger, IUsuarioRepository repositorioUsuario)
    {
        _logger = logger;
        _repositorioUsuario = repositorioUsuario;
    }
    public IActionResult Index()
    {
        return View(new LoginViewModel());
    }
    public IActionResult Login(Usuario usuario)
    {
        if (!ModelState.IsValid)
        {
            return RedirectToAction("Index");
        }
        //existe el usuario?
        List<Usuario> usuarios = _repositorioUsuario.GetAll();
        var usuarioLogin = usuarios.FirstOrDefault(u => u.NombreDeUsuario == usuario.NombreDeUsuario && u.Contrasenia == usuario.Contrasenia);
        // si el usuario no existe devuelvo al index
        if (usuarioLogin == null) return RedirectToAction("Index");
        //Registro el usuario
        logearUsuario(usuarioLogin);
        //Devuelvo el usuario al Home
        return RedirectToRoute(new { controller = "Home", action = "Index" });
    }

    private void logearUsuario(Usuario usuario)
    {
        HttpContext.Session.SetInt32("Id", usuario.Id);
        HttpContext.Session.SetString("NombreDeUsuario", usuario.NombreDeUsuario);
        HttpContext.Session.SetString("Rol", usuario.Rol.ToString());
    }
}