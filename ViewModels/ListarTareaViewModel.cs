using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using tl2_tp10_2023_LucianoCV01.Models;

namespace tl2_tp10_2023_LucianoCV01.ViewModels
{
    public class ListarTareaViewModel
    {
        public List<TareaViewModel> Tareas { get; set; }

        public ListarTareaViewModel()
        {
            Tareas = new List<TareaViewModel>();
        }
        public ListarTareaViewModel(List<Tarea> tareas)
        {
            Tareas = tareas.Select(t => new TareaViewModel(t)).ToList();
        }
    }
}