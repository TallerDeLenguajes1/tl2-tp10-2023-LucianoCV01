namespace tl2_tp10_2023_LucianoCV01.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string NombreDeUsuario { get; set; }
        public string Contrasenia { get; set; }
        public Rol Rol { get; set; }
    }
    public enum Rol
    {
        operador,   
        administrador
    }
}