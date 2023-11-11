namespace tl2_tp10_2023_LucianoCV01.Models
{
    public class Usuario
    {
        int id;
        string? nombreDeUsuario;

        // Propiedades
        public int Id { get => id; set => id = value; }
        public string? NombreDeUsuario { get => nombreDeUsuario; set => nombreDeUsuario = value; }
    }
}