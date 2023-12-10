using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using tl2_tp10_2023_LucianoCV01.Models;

namespace tl2_tp10_2023_LucianoCV01.ViewModels
{
    public class ModificarTableroViewModel
    {
        public ModificarTableroViewModel(Tablero tablero)
        {
            Id = tablero.Id;
            Nombre = tablero.Nombre;
            Descripcion = tablero.Descripcion;
        }

        [Required(ErrorMessage = "Este campo es requerido.")]
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Este campo es requerido.")]
        [StringLength(60)]
        [Display(Name = "Nombre")]
        public string Nombre { get; set; }

        [StringLength(100)]
        [Display(Name = "Descripción")]
        public string? Descripcion { get; set; }

    }
}