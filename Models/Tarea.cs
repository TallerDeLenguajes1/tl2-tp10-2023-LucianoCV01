using tl2_tp10_2023_LucianoCV01.ViewModels;
namespace tl2_tp10_2023_LucianoCV01.Models
{
    public class Tarea
    {
        public Tarea()
        {
        }

        public Tarea(CrearTareaViewModel tarea)
        {
            Nombre = tarea.Nombre;
            Estado = tarea.Estado;
            Descripcion = tarea.Descripcion;
            Color = tarea.Color;
            IdUsuarioAsignado = tarea.IdUsuarioAsignado;
        }

        public int Id { get; set; }
        public int IdTablero { get; set; }
        public string Nombre { get; set; }
        public Estado Estado { get; set; }
        public string? Descripcion { get; set; }
        public string? Color { get; set; }
        public int? IdUsuarioAsignado { get; set; }

    }
    public enum Estado
    {
        Ideas,
        ToDo,
        Doing,
        Review,
        Done
    }
}