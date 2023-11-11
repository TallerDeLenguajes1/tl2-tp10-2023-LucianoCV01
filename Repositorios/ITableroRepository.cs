using tl2_tp10_2023_LucianoCV01.Models;

namespace EspacioRepositorios
{
    public interface ITableroRepository
    {
        public void Create(Tablero tablero); // ● Crear un nuevo tablero (devuelve un objeto Tablero)
        public void Update(int id, Tablero tablero); // ● Modificar un tablero existente (recibe un id y un objeto Tablero)
        public Tablero GetById(int id); // ● Obtener detalles de un tablero por su ID. (recibe un id y devuelve un Tablero)
        public List<Tablero> GetAll(); // ● Listar todos los tableros existentes (devuelve un list de tableros)
        public List<Tablero> GetByIdUsuario(int idUsuario); // ● Listar todos los tableros de un usuario específico. (recibe un IdUsuario, devuelve un list de tableros)
        public void Remove(int id); // ● Eliminar un tablero por ID
    }
}