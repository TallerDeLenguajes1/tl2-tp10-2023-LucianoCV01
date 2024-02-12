using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using tl2_tp10_2023_LucianoCV01.Models;

namespace tl2_tp10_2023_LucianoCV01.ViewModels
{
    public class TareaViewModel
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public EstadoTarea Estado { get; set; }
        public string? Descripcion { get; set; }
        public ColorEtiqueta Color { get; set; }
        public int? IdUsuarioAsignado { get; set; }

        public TareaViewModel()
        {
        }
        public TareaViewModel(Tarea tarea)
        {
            Id = tarea.Id;
            Nombre = tarea.Nombre;
            Estado = tarea.Estado;
            Descripcion = tarea.Descripcion;
            Color = tarea.Color;
            IdUsuarioAsignado = tarea.IdUsuarioAsignado;
        }
    }
}