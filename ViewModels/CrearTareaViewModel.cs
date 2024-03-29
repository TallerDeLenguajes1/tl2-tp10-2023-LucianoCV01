using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using tl2_tp10_2023_LucianoCV01.Models;

namespace tl2_tp10_2023_LucianoCV01.ViewModels
{
    public class CrearTareaViewModel
    {
        public List<UsuarioViewModel>? UsuariosDisponibles { get; set; }

        [Required(ErrorMessage = "Este campo es requerido.")]
        [Display(Name = "Id Tablero")]
        public int IdTablero { get; set; }

        [Required(ErrorMessage = "Este campo es requerido.")]
        [StringLength(60)]
        [Display(Name = "Nombre")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Este campo es requerido.")]
        [Display(Name = "Estado")]
        public EstadoTarea Estado { get; set; }

        [StringLength(100)]
        [Display(Name = "Descripcion")]
        public string? Descripcion { get; set; }

        [Display(Name = "Color")]
        public ColorEtiqueta Color { get; set; }

        [Display(Name = "Id Usuario Asignado")]
        public int? IdUsuarioAsignado { get; set; }
    }
}