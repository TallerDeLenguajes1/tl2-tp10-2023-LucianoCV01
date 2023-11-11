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
    public IActionResult ListarUsuario(){
        return View(manejoDeUsuarios.GetAll());
    }
    // Controlador CREAR
    [HttpGet]
    public IActionResult CrearUsuario(){
        return View(new Usuario());
    }
    [HttpPost]
    public IActionResult CrearUsuario(Usuario u){
        manejoDeUsuarios.Create(u);
        return RedirectToAction("ListarUsuario");
    }

    // ------------------------------------------>

    [HttpPost("api/usuario")] //Que devuelvo???
    public ActionResult<Usuario> AgregarUsuario(Usuario u)
    {
        manejoDeUsuarios.Create(u);
        return Ok(u);
    }
    [HttpGet]
    [Route("api/usuario")]
    public ActionResult<List<Usuario>> GetListadoUsuario()
    {
        var listaUsuarios = manejoDeUsuarios.GetAll();
        return Ok(listaUsuarios);
    }
    [HttpGet]
    [Route("api/usuario/{Id}")]
    public ActionResult<Usuario> GetUsuarioPorId(int Id)
    {
        var usuarioBuscado = manejoDeUsuarios.GetById(Id);
        return Ok(usuarioBuscado);
    }
    [HttpPut("api/tarea/{Id}/Nombre")] //Que devuelvo???
    public ActionResult<Usuario> ActualizarTarea(int Id, Usuario usuarioActualizado)
    {
        manejoDeUsuarios.Update(Id, usuarioActualizado);
        return Ok(usuarioActualizado);
    }
}