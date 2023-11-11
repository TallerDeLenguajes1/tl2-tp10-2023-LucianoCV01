using tl2_tp10_2023_LucianoCV01.Models;

namespace EspacioRepositorios
{
    public interface ITareaRepository
    {
        public void Create(int idTablero, Tarea tarea); // ● Crear una nueva tarea en un tablero. (recibe un idTablero, devuelve un objeto Tarea)
        public void Update(int id, Tarea tarea); // ● Modificar una tarea existente. (recibe un id y un objeto Tarea)
        public Tarea GetById(int id); // ● Obtener detalles de una tarea por su ID. (devuelve un objeto Tarea)
        public List<Tarea> GetByIdUsuario(int idUsuario); // ● Listar todas las tareas asignadas a un usuario específico.(recibe un idUsuario, devuelve un list de tareas)
        public List<Tarea> GetByIdTablero(int idTablero); // ● Listar todas las tareas de un tablero específico. (recibe un idTablero, devuelve un list de tareas)
        public void Remove(int id); // ● Eliminar una tarea (recibe un IdTarea)
        public void AsignarUsuario(int idTarea, int idUsuario); // ● Asignar Usuario a Tarea (recibe idUsuario y un idTarea)
        public List<Tarea> GetByEstado(int estado);
    }
}