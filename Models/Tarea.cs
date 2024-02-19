using tl2_tp10_2023_LucianoCV01.ViewModels;
namespace tl2_tp10_2023_LucianoCV01.Models
{
    public class Tarea
    {
        public Tarea()
        {
        }
        public Tarea(CrearTareaViewModel t)
        {
            IdTablero = t.IdTablero;
            Nombre = t.Nombre;
            Estado = t.Estado;
            Descripcion = t.Descripcion;
            Color = t.Color;
            IdUsuarioAsignado = t.IdUsuarioAsignado;
        }
        public Tarea(ModificarTareaViewModel t)
        {
            Id = t.Id;
            IdTablero = t.IdTablero;
            Nombre = t.Nombre;
            Estado = t.Estado;
            Descripcion = t.Descripcion;
            Color = t.Color;
            IdUsuarioAsignado = t.IdUsuarioAsignado;
        }

        public int Id { get; set; }
        public int IdTablero { get; set; }
        public string Nombre { get; set; }
        public EstadoTarea Estado { get; set; }
        public string? Descripcion { get; set; }
        public ColorEtiqueta Color { get; set; }
        public int? IdUsuarioAsignado { get; set; }
    }
    public enum EstadoTarea
    {
        Ideas,
        ToDo,
        Doing,
        Review,
        Done
    }
    public enum ColorEtiqueta
    {
        Ninguno,
        Rojo,
        Naranja,
        Amarillo,
        Verde,
        Celeste,
        Azul,
        Violeta
    }
}