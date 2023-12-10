using Microsoft.Data.Sqlite;
using tl2_tp10_2023_LucianoCV01.Models;
namespace tl2_tp10_2023_LucianoCV01.Repository
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private const string cadenaConexion = "Data Source=DB/Kanban.db;Cache=Shared";
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
                            NombreDeUsuario = reader["nombre_de_usuario"].ToString()!,
                            Contrasenia = reader["contrasenia"].ToString()!,
                            Rol =  (Rol)Convert.ToInt32(reader["rol"])
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
            var usuario = new Usuario();
            using (SqliteConnection connection = new SqliteConnection(cadenaConexion))
            {
                SqliteCommand command = new SqliteCommand(queryString, connection);
                command.Parameters.Add(new SqliteParameter("@id", id));
                connection.Open();
                using (SqliteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        usuario.Id = Convert.ToInt32(reader["id"]);
                        usuario.NombreDeUsuario = reader["nombre_de_usuario"].ToString()!;
                        usuario.Contrasenia = reader["contrasenia"].ToString()!;
                        usuario.Rol = (Rol)Convert.ToInt32(reader["rol"]);
                    }
                }
                connection.Close();
            };
            return usuario;
        }
        public void Create(Usuario usuario)
        {
            const string queryString = $"INSERT INTO Usuario (nombre_de_usuario, contrasenia, rol) VALUES (@name, @pass, @rol)";
            using (SqliteConnection connection = new SqliteConnection(cadenaConexion))
            {
                SqliteCommand command = new SqliteCommand(queryString, connection);
                command.Parameters.Add(new SqliteParameter("@name", usuario.NombreDeUsuario));
                command.Parameters.Add(new SqliteParameter("@pass", usuario.Contrasenia));
                command.Parameters.Add(new SqliteParameter("@rol", usuario.Rol));
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
        }
        public void Update(int id, Usuario usuario)
        {
            const string queryString = $"UPDATE Usuario SET nombre_de_usuario = (@name), contrasenia = (@pass), rol = (@rol) WHERE id = (@id);";
            using (SqliteConnection connection = new SqliteConnection(cadenaConexion))
            {
                SqliteCommand command = new SqliteCommand(queryString, connection);
                command.Parameters.Add(new SqliteParameter("@name", usuario.NombreDeUsuario));
                command.Parameters.Add(new SqliteParameter("@pass", usuario.Contrasenia));
                command.Parameters.Add(new SqliteParameter("@rol", usuario.Rol));
                command.Parameters.Add(new SqliteParameter("@id", id));
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
        }
        public void Remove(int id)
        {
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
