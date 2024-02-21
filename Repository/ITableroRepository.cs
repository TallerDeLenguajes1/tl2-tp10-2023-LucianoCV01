using tl2_tp10_2023_LucianoCV01.Models;
namespace tl2_tp10_2023_LucianoCV01.Repository
{
    public interface ITableroRepository
    {
        public List<Tablero> GetAll();
        public Tablero GetById(int id);
        public List<Tablero> GetByIdUsuario(int idUsuario);
        public List<Tablero> GetByIdUsuarioParticipante(int idUsuario);
        public List<Tablero> GetByIdUsuarioAjenos(int idUsuario);
        public List<Usuario> GetByIdTableroSusParticipantes(int idTablero);
        public void Create(Tablero tablero);
        public void Update(int id, Tablero tablero);
        public void Delete(int id);
        public void DeleteTareas(int id);
        public void DeleteTablerosPorUsuario(int id);
    }
}