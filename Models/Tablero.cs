using tl2_tp10_2023_LucianoCV01.ViewModels;
namespace tl2_tp10_2023_LucianoCV01.Models
{
    public class Tablero
    {
        public int Id { get; set; }
        public int IdUsuarioPropietario { get; set; }
        public string Nombre { get; set; }
        public string? Descripcion { get; set; }

        public Tablero()
        {
        }
        public Tablero(CrearTableroViewModel t)
        {
            Nombre = t.Nombre;
            Descripcion = t.Descripcion;
        }
    }
}