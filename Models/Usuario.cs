namespace tl2_tp10_2023_LucianoCV01.Models
{
    public class Usuario
    {
        int id;
        string? nombreDeUsuario;
        string? contrasenia;
        Rol rol;
        // Propiedades
        public int Id { get => id; set => id = value; }
        public string? NombreDeUsuario { get => nombreDeUsuario; set => nombreDeUsuario = value; }
        public string? Contrasenia { get => contrasenia; set => contrasenia = value; }
        public Rol Rol { get => rol; set => rol = value; }
    }
    public enum Rol
    {
        admin = 0,
        operador = 1
    }
}