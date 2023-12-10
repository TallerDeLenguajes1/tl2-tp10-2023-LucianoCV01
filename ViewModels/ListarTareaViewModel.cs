using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using tl2_tp10_2023_LucianoCV01.Models;

namespace tl2_tp10_2023_LucianoCV01.ViewModels
{
    public class ListarTareaViewModel
    {
        public ListarTareaViewModel()
        {
        }
        public ListarTareaViewModel(Tarea tarea)
        {
            Id = tarea.Id;
            Nombre = tarea.Nombre;
            Estado = tarea.Estado;
            Descripcion = tarea.Descripcion;
            Color = tarea.Color;
            IdUsuarioAsignado = tarea.IdUsuarioAsignado;
        }

        public int Id { get; set; }
        public string Nombre { get; set; }
        public Estado Estado { get; set; }
        public string? Descripcion { get; set; }
        public string? Color { get; set; }
        public int? IdUsuarioAsignado { get; set; }

        public List<ListarTareaViewModel> convertirLista(List<Tarea> tareas)
        {
            List<ListarTareaViewModel> listadoTareas = new();
            foreach (var tarea in tareas)
            {
                listadoTareas.Add(new ListarTareaViewModel(tarea));
            }
            return listadoTareas;
        }
    }
}