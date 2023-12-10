using tl2_tp10_2023_LucianoCV01.Models;
namespace tl2_tp10_2023_LucianoCV01.Repository
{
    public interface ITableroRepository
    {
        public List<Tablero> GetAll();
        public Tablero GetById(int id);
        public List<Tablero> GetByIdUsuario(int idUsuario);
        public void Create(Tablero tablero);
        public void Update(int id, Tablero tablero);
        public void Remove(int id);
    }
}