using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using tl2_tp10_2023_LucianoCV01.Models;

namespace tl2_tp10_2023_LucianoCV01.ViewModels
{
    public class ModificarUsuarioViewModel
    {
        [Required(ErrorMessage = "Este campo es requerido.")]
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Este campo es requerido.")]
        [StringLength(60)]
        [Display(Name = "Nombre de Usuario")]
        public string NombreDeUsuario { get; set; }

        [Required(ErrorMessage = "Este campo es requerido.")]
        [StringLength(60)]
        [PasswordPropertyText]
        [Display(Name = "Contraseña")]
        public string Contrasenia { get; set; }

        [Display(Name = "Rol")]
        public Rol Rol { get; set; }


        public ModificarUsuarioViewModel()
        {
        }
        public ModificarUsuarioViewModel(Usuario usuario)
        {
            Id = usuario.Id;
            NombreDeUsuario = usuario.NombreDeUsuario;
            Contrasenia = usuario.Contrasenia;
            Rol = usuario.Rol;
        }
    }
}