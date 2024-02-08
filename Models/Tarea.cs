namespace tl2_tp10_2023_LucianoCV01.Models
{
    class Tarea
    {
        public int Id { get; set; }
        public int IdTablero { get; set; }
        public string Nombre { get; set; }
        public EstadoTarea Estado { get; set; }
        public string Descripcion { get; set; }
        public ColorEtiqueta Color { get; set; }
        public int IdUsuarioAsignado { get; set; }
    }
    public enum EstadoTarea
    {
        Ideas, 
        ToDo, 
        Doing, 
        Review, 
        Done
    }
    public enum ColorEtiqueta
    {
        Rojo, 
        Naranja, 
        Amarillo, 
        Verde, 
        Celeste, 
        Azul, 
        Violeta
    }
}