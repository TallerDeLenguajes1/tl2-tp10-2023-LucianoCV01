using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using tl2_tp10_2023_LucianoCV01.Models;

namespace tl2_tp10_2023_LucianoCV01.ViewModels
{
    public class ListarUsuarioViewModel
    {
        public int Id { get; set; }
        public string NombreDeUsuario { get; set; }
        public Rol Rol { get; set; }

        public ListarUsuarioViewModel()
        {
        }
        public ListarUsuarioViewModel(Usuario usuario)
        {
            Id = usuario.Id;
            NombreDeUsuario = usuario.NombreDeUsuario;
            Rol = usuario.Rol;
        }
        public List<ListarUsuarioViewModel> convertirLista(List<Usuario> usuarios)
        {
            List<ListarUsuarioViewModel> listadoUsuarios = new();
            foreach (var usuario in usuarios)
            {
                listadoUsuarios.Add(new ListarUsuarioViewModel(usuario));
            }
            return listadoUsuarios;
        }
    }
}