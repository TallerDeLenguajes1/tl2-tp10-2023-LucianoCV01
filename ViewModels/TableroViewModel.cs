using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using tl2_tp10_2023_LucianoCV01.Models;

namespace tl2_tp10_2023_LucianoCV01.ViewModels
{
    public class TableroViewModel
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string? Descripcion { get; set; }

        public TableroViewModel()
        {
        }
        public TableroViewModel(Tablero tablero)
        {
            Id = tablero.Id;
            Nombre = tablero.Nombre;
            Descripcion = tablero.Descripcion;
        }
    }
}