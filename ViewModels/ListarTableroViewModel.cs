using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using tl2_tp10_2023_LucianoCV01.Models;

namespace tl2_tp10_2023_LucianoCV01.ViewModels
{
    public class ListarTableroViewModel
    {
        public List<TableroViewModel> TablerosPropios { get; set; }
        public List<TableroViewModel> TablerosAjenos { get; set; }

        public ListarTableroViewModel()
        {
            TablerosPropios = new List<TableroViewModel>();
            TablerosAjenos = new List<TableroViewModel>();
        }
        public ListarTableroViewModel(List<Tablero> tableros, int? idUsuarioPropietario)
        {
            // Tableros = tableros.Select(t => new TableroViewModel(t)).ToList();
            TablerosPropios = new List<TableroViewModel>();
            TablerosAjenos = new List<TableroViewModel>();

            foreach (var tablero in tableros)
            {
                var tableroViewModel = new TableroViewModel(tablero);
                
                // Comprobamos si el tablero es propio o ajeno
                if (tablero.IdUsuarioPropietario == idUsuarioPropietario)
                {
                    TablerosPropios.Add(tableroViewModel);
                }
                else
                {
                    TablerosAjenos.Add(tableroViewModel);
                }
            }
        }
    }
}