using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using tl2_tp10_2023_LucianoCV01.Models;

namespace tl2_tp10_2023_LucianoCV01.ViewModels
{
    public class ListarTareaViewModel
    {
        public int IdTablero { get; set; }
        public List<TareaViewModel> Tareas { get; set; }

        public ListarTareaViewModel()
        {
            IdTablero = -9999;
            Tareas = new List<TareaViewModel>();
        }
        public ListarTareaViewModel(int idTablero, List<Tarea> tareas)
        {
            IdTablero = idTablero;
            Tareas = tareas.Select(t => new TareaViewModel(t)).ToList();
        }
    }
}