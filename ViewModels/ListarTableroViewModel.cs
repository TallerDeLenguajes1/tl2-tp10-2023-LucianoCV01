using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using tl2_tp10_2023_LucianoCV01.Models;

namespace tl2_tp10_2023_LucianoCV01.ViewModels
{
    public class ListarTableroViewModel
    {
        public List<TableroViewModel> Tableros { get; set; }

        public ListarTableroViewModel()
        {
            Tableros = new List<TableroViewModel>();
        }
        public ListarTableroViewModel(List<Tablero> tableros)
        {
            Tableros = tableros.Select(t => new TableroViewModel(t)).ToList();
        }
    }
}