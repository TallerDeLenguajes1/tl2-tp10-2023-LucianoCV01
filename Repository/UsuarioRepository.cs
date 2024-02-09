using Microsoft.Data.Sqlite;
using tl2_tp10_2023_LucianoCV01.Models;

namespace tl2_tp10_2023_LucianoCV01.Repository
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private string cadenaConexion = "Data Source=DB/Taskmaster.db;Cache=Shared";
        public List<Usuario> GetAll()
        {
            const string queryString = @"SELECT * FROM Usuario;";
            List<Usuario> usuarios = new List<Usuario>();
            using (SqliteConnection connection = new SqliteConnection(cadenaConexion))
            {
                SqliteCommand command = new SqliteCommand(queryString, connection);
                connection.Open();
                using (SqliteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var usuario = new Usuario()
                        {
                            Id = Convert.ToInt32(reader["id"]),
                            NombreDeUsuario = reader["nombreDeUsuario"].ToString()!,
                        };
                        usuarios.Add(usuario);
                    }
                }
                connection.Close();
            }
            return usuarios;
        }
        public Usuario GetById(int id)
        {
            const string queryString = $"SELECT * FROM Usuario WHERE id = @id";
            Usuario? usuario = null;
            using (SqliteConnection connection = new SqliteConnection(cadenaConexion))
            {
                SqliteCommand command = new SqliteCommand(queryString, connection);
                command.Parameters.Add(new SqliteParameter("@id", id));
                connection.Open();
                using (SqliteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        usuario = new Usuario
                        {
                            Id = Convert.ToInt32(reader["id"]),
                            NombreDeUsuario = reader["nombreDeUsuario"].ToString()!,
                        };
                    }
                }
                connection.Close();
            }
            return usuario; // Lanzar excepcion de error como nulo
        }
        public void Create(Usuario usuario)
        {
            const string queryString = $"INSERT INTO Usuario (nombreDeUsuario) VALUES (@name)";
            using (SqliteConnection connection = new SqliteConnection(cadenaConexion))
            {
                SqliteCommand command = new SqliteCommand(queryString, connection);
                command.Parameters.Add(new SqliteParameter("@name", usuario.NombreDeUsuario));
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
        }
        public void Update(int id, Usuario usuario)
        {
            // lanzar excepcion por si no existe el usuario a modificar
            const string queryString = $"UPDATE Usuario SET nombreDeUsuario = (@name) WHERE id = (@id);";
            using (SqliteConnection connection = new SqliteConnection(cadenaConexion))
            {
                SqliteCommand command = new SqliteCommand(queryString, connection);
                command.Parameters.Add(new SqliteParameter("@name", usuario.NombreDeUsuario));
                command.Parameters.Add(new SqliteParameter("@id", id));
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
        }
        public void Delete(int id)
        {
            // lanzar excepcion si el usuario a eliminar no existe
            const string queryString = $"DELETE FROM Usuario WHERE id = @id;";
            using (SqliteConnection connection = new SqliteConnection(cadenaConexion))
            {
                SqliteCommand command = new SqliteCommand(queryString, connection);
                command.Parameters.Add(new SqliteParameter("@id", id));
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
        }
    }
}