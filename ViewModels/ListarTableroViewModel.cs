using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using tl2_tp10_2023_LucianoCV01.Models;

namespace tl2_tp10_2023_LucianoCV01.ViewModels
{
    public class ListarTableroViewModel
    {
        public List<TableroViewModel> TablerosPropios { get; set; }
        public List<TableroViewModel> TablerosParticipo { get; set; }
        public List<TableroViewModel> TablerosAjenos { get; set; }

        public ListarTableroViewModel()
        {
            TablerosPropios = new List<TableroViewModel>();
            TablerosAjenos = new List<TableroViewModel>();
        }
        public ListarTableroViewModel(List<Tablero> tablerosPropios, List<Tablero> tablerosParticipando, List<Tablero> tablerosAjenos)
        {
            TablerosPropios = tablerosPropios.Select(t => new TableroViewModel(t)).ToList();
            TablerosParticipo = tablerosParticipando.Select(t => new TableroViewModel(t)).ToList();
            TablerosAjenos = tablerosAjenos.Select(t => new TableroViewModel(t)).ToList();
        }
    }
}