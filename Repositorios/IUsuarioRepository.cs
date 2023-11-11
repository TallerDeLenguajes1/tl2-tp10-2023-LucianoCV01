using tl2_tp10_2023_LucianoCV01.Models;

namespace EspacioRepositorios
{
    public interface IUsuarioRepository
    {

        public void Create(Usuario usuario); // ● Crear un nuevo usuario. (recibe un objeto Usuario)
        public void Update(int id, Usuario usuario); // ● Modificar un usuario existente. (recibe un Id y un objeto Usuario)
        public List<Usuario> GetAll(); // ● Listar todos los usuarios registrados. (devuelve un List de Usuarios)
        public Usuario GetById(int id); // ● Obtener detalles de un usuario por su ID. (recibe un Id y devuelve un Usuario)
        public void Remove(int id); // ● Eliminar un usuario por ID

    }
}