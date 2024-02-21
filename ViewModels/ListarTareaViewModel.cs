using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using tl2_tp10_2023_LucianoCV01.Models;

namespace tl2_tp10_2023_LucianoCV01.ViewModels
{
    public class ListarTareaViewModel
    {
        public int IdTablero { get; set; }
        public int IdDuenio { get; set; }
        public ListarUsuarioViewModel Usuarios { get; set; }
        public List<TareaViewModel> Tareas { get; set; }

        public ListarTareaViewModel()
        {
            IdTablero = -9999;
            IdDuenio = -9999;
            Usuarios = new();
            Tareas = new List<TareaViewModel>();
        }
        public ListarTareaViewModel(int idTablero, int idDuenio, List<Usuario> usuariosSimples, List<Tarea> tareas)
        {
            IdTablero = idTablero;
            IdDuenio = idDuenio;
            Usuarios = new(usuariosSimples);
            Tareas = tareas.Select(t => new TareaViewModel(t)).ToList();
        }
    }
}