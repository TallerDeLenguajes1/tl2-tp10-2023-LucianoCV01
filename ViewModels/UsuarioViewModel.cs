using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using tl2_tp10_2023_LucianoCV01.Models;

namespace tl2_tp10_2023_LucianoCV01.ViewModels
{
    public class UsuarioViewModel
    {
        public int Id { get; set; }
        public string NombreDeUsuario { get; set; }
        public Rol Rol { get; set; }

        public UsuarioViewModel()
        {
        }
        public UsuarioViewModel(Usuario usuario)
        {
            Id = usuario.Id;
            NombreDeUsuario = usuario.NombreDeUsuario;
            Rol = usuario.Rol;
        }
    }
}