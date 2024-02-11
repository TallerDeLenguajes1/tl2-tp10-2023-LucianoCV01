using tl2_tp10_2023_LucianoCV01.ViewModels;
namespace tl2_tp10_2023_LucianoCV01.Models
{
    public class Usuario
    {
        public Usuario()
        {
        }
        public Usuario(CrearUsuarioViewModel u)
        {
            NombreDeUsuario = u.NombreDeUsuario;
            Contrasenia = u.Contrasenia;
            Rol = u.Rol;
        }
        public Usuario(ModificarUsuarioViewModel u)
        {
            Id = u.Id;
            NombreDeUsuario = u.NombreDeUsuario;
            Contrasenia = u.Contrasenia;
            Rol = u.Rol;
        }

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