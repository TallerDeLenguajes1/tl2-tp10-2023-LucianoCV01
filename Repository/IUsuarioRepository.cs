using tl2_tp10_2023_LucianoCV01.Models;
namespace tl2_tp10_2023_LucianoCV01.Repository
{
    public interface IUsuarioRepository
    {
        public List<Usuario> GetAll ();
        public Usuario GetById (int id);
        public void Create (Usuario usuario);
        public void Update (Usuario usuario);
        public void Delete (int id);
    }
}