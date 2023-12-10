using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using tl2_tp10_2023_LucianoCV01.Models;

namespace tl2_tp10_2023_LucianoCV01.ViewModels
{
    public class ListarTableroViewModel
    {
        public ListarTableroViewModel()
        {
        }
        public ListarTableroViewModel(Tablero tablero)
        {
            Id = tablero.Id;
            Nombre = tablero.Nombre;
            Descripcion = tablero.Descripcion;
        }

        public int Id { get; set; }
        public string Nombre { get; set; }
        public string? Descripcion { get; set; }

        public List<ListarTableroViewModel> convertirLista(List<Tablero> tableros)
        {
            List<ListarTableroViewModel> listadoTableros = new();
            foreach (var tablero in tableros)
            {
                listadoTableros.Add(new ListarTableroViewModel(tablero));
            }
            return listadoTableros;
        }
    }
}