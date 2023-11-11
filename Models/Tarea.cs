namespace tl2_tp10_2023_LucianoCV01.Models
{
    public class Tarea
    {
        int id;
        int idTablero;
        string? nombre;
        EstadoTarea estado;
        string? descripcion;
        string? color;
        int idUsuarioAsignado;

        // Propiedades
        public int Id { get => id; set => id = value; }
        public int IdTablero { get => idTablero; set => idTablero = value; }
        public string? Nombre { get => nombre; set => nombre = value; }
        public EstadoTarea Estado { get => estado; set => estado = value; }
        public string? Descripcion { get => descripcion; set => descripcion = value; }
        public string? Color { get => color; set => color = value; }
        public int IdUsuarioAsignado { get => idUsuarioAsignado; set => idUsuarioAsignado = value; }
    }
    public enum EstadoTarea
    {
        Ideas,
        ToDo, 
        Doing, 
        Review,
        Done
    }
}