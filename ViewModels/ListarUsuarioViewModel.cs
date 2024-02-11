using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using tl2_tp10_2023_LucianoCV01.Models;

namespace tl2_tp10_2023_LucianoCV01.ViewModels
{
    public class ListarUsuarioViewModel
    {
        public List<UsuarioViewModel> Usuarios { get; set; }

        public ListarUsuarioViewModel()
        {
            Usuarios = new List<UsuarioViewModel>();
        }
        public ListarUsuarioViewModel(List<Usuario> usuarios)
        {
            Usuarios = usuarios.Select(u => new UsuarioViewModel(u)).ToList();
        }
    }
}