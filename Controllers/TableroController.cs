// using Microsoft.AspNetCore.Mvc;
// using tl2_tp10_2023_LucianoCV01.Models;
// using EspacioRepositorios;

// namespace tl2_tp10_2023_LucianoCV01.Controllers;

// [ApiController]
// [Route("[controller]")]
// public class TableroController : ControllerBase
// {
//     private TableroRepository manejoDeTableros; // Utilizo la clase o la interfaz?
//     private readonly ILogger<TableroController> _logger;

//     public TableroController(ILogger<TableroController> logger)
//     {
//         _logger = logger;
//         manejoDeTableros = new();
//     }

//     [HttpPost("api/Tablero")] //Que devuelvo???
//     public ActionResult<Tablero> AgregarTablero(Tablero t)
//     {
//         manejoDeTableros.Create(t);
//         return Ok(t);
//     }
//     [HttpGet]
//     [Route("api/tableros")]
//     public ActionResult<List<Tablero>> GetListadoTablero()
//     {
//         var listaTableros = manejoDeTableros.GetAll();
//         return Ok(listaTableros);
//     }
// }