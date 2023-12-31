using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using tl2_tp10_2023_LucianoCV01.Models;

namespace tl2_tp10_2023_LucianoCV01.ViewModels
{
    public class ModificarTareaViewModel
    {
        public ModificarTareaViewModel(Tarea tarea)
        {
            Id = tarea.Id;
            Nombre = tarea.Nombre;
            Estado = tarea.Estado;
            Descripcion = tarea.Descripcion;
            Color = tarea.Color;
            IdUsuarioAsignado = tarea.IdUsuarioAsignado;
        }

        [Required(ErrorMessage = "Este campo es requerido.")]
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Este campo es requerido.")]
        [StringLength(60)]
        [Display(Name = "Nombre")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Este campo es requerido.")]
        [Display(Name = "Estado")]
        public Estado Estado { get; set; }

        [StringLength(100)]
        [Display(Name = "Descripcion")]
        public string? Descripcion { get; set; }

        [StringLength(30)]
        [Display(Name = "Color")]
        public string? Color { get; set; }

        [Display(Name = "Id Usuario Asignado")]
        public int? IdUsuarioAsignado { get; set; }
    }
}