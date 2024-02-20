using tl2_tp10_2023_LucianoCV01.ViewModels;
namespace tl2_tp10_2023_LucianoCV01.Models
{
    public class Tablero
    {
        public Tablero()
        {
        }
        public Tablero(CrearTableroViewModel t)
        {
            IdUsuarioPropietario = t.IdUsuarioPropietario;
            Nombre = t.Nombre;
            Descripcion = t.Descripcion;
        }
        public Tablero(ModificarTableroViewModel t)
        {
            Id = t.Id;
            Nombre = t.Nombre;
            Descripcion = t.Descripcion;
        }

        public int Id { get; set; }
        public int IdUsuarioPropietario { get; set; }
        public string Nombre { get; set; }
        public string? Descripcion { get; set; }
    }
}