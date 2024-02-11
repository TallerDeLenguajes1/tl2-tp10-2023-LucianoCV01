using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using tl2_tp10_2023_LucianoCV01.Models;

namespace tl2_tp10_2023_LucianoCV01.ViewModels
{
    public class CrearUsuarioViewModel
    {
        [Required(ErrorMessage = "Este campo es requerido.")]
        [StringLength(60)]
        [Display(Name = "Nombre de Usuario")] 
        public string NombreDeUsuario {get;set;}        

        [Required(ErrorMessage = "Este campo es requerido.")]
        [StringLength(60)]
        [PasswordPropertyText]
        [Display(Name = "Contraseña")]
        public string Contrasenia {get;set;}

        [Display(Name = "Rol")]
        public Rol Rol {get;set;}
    }
}