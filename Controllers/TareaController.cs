// using Microsoft.AspNetCore.Mvc;
// using tl2_tp10_2023_LucianoCV01.Models;
// using EspacioRepositorios;

// namespace tl2_tp10_2023_LucianoCV01.Controllers;

// [ApiController]
// [Route("[controller]")]
// public class TareaController : ControllerBase
// {
//     private TareaRepository manejoDeTareas; // Utilizo la clase o la interfaz?
//     private readonly ILogger<TareaController> _logger;

//     public TareaController(ILogger<TareaController> logger)
//     {
//         _logger = logger;
//         manejoDeTareas = new();
//     }

//     [HttpPost("api/tarea")] //Que devuelvo??? Le cambio el nombre a la ruta?
//     public ActionResult<Tarea> AgregarUsuario(int idTablero, Tarea t)
//     {
//         manejoDeTareas.Create(idTablero, t);
//         return Ok(t);
//     }

//     [HttpPut("api/Tarea/{id}/Nombre/{Nombre}")] //Que devuelvo??? Esta bien hecho??
//     public ActionResult<int> ActualizarTareaPorNombre(int id, string Nombre)
//     {
//         manejoDeTareas.ActualizarNombre(id, Nombre);
//         return Ok(id);
//     }

//     [HttpPut("api/Tarea/{id}/Estado/{estado}")] //Que devuelvo??? Esta bien hecho??
//     public ActionResult<int> ActualizarTareaPorEstado(int id, int estado)
//     {
//         manejoDeTareas.ActualizarEstado(id, estado);
//         return Ok(id);
//     }

//     [HttpDelete("api/Tarea/{id}")] // Que devuelvo??
//     public ActionResult<int> EliminarTareaPorId(int id)
//     {
//         manejoDeTareas.Remove(id);
//         return Ok(id);
//     }

//     [HttpGet]
//     [Route("api/Tarea/{Estado}")] // bien hecho??
//     public ActionResult<int> GetCantidadPorEstado(int Estado)
//     {
//         var listaTareas = manejoDeTareas.GetByEstado(Estado);
//         return Ok(listaTareas.Count);
//     }

//     [HttpGet]
//     [Route("api/Tarea/Usuario/{Id}")]
//     public ActionResult<List<Tarea>> GetTareaPorIdUsuario(int Id)
//     {
//         var listaTareas = manejoDeTareas.GetByIdUsuario(Id);
//         return Ok(listaTareas);
//     }

//     [HttpGet]
//     [Route("api/Tarea/Tablero/{Id}")]
//     public ActionResult<List<Tarea>> GetTareaPorIdTablero(int Id)
//     {
//         var listaTareas = manejoDeTareas.GetByIdTablero(Id);
//         return Ok(listaTareas);
//     }
// }