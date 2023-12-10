using tl2_tp10_2023_LucianoCV01.Models;
namespace tl2_tp10_2023_LucianoCV01.Repository
{
    public interface ITareaRepository
    {
        public List<Tarea> GetAll();
        public Tarea GetById(int id);
        public List<Tarea> GetByIdUsuario(int idUsuario);
        public List<Tarea> GetByIdTablero(int idTablero);
        public void Create(int idTablero, Tarea tarea);
        public void Update(int id, Tarea tarea);
        public void UpdateUsuario(int id, int idUsuario);
        public void Remove(int id);
    }
}