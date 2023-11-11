namespace tl2_tp10_2023_LucianoCV01.Models
{
    public class Tablero
    {
        int id;
        int idUsuarioPropietario;
        string? nombre;
        string? descripcion;

        // Propiedades
        public int Id { get => id; set => id = value; }
        public int IdUsuarioPropietario { get => idUsuarioPropietario; set => idUsuarioPropietario = value; }
        public string? Nombre { get => nombre; set => nombre = value; }
        public string? Descripcion { get => descripcion; set => descripcion = value; }
    }
}