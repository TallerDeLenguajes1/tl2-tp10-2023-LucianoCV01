using Microsoft.Data.Sqlite;
using tl2_tp10_2023_LucianoCV01.Models;

namespace EspacioRepositorios
{
    class UsuarioRepository : IUsuarioRepository
    {
        private string cadenaConexion = "Data Source=DB/TP08-CosentinoLuciano.db;Cache=Shared";

        public void Create(Usuario usuario)
        {
            var query = $"INSERT INTO Usuario (nombre_de_usuario, contrasenia, rol) VALUES (@name, @contrasenia, @rol)";
            using (SqliteConnection connection = new SqliteConnection(cadenaConexion))
            {

                connection.Open();
                var command = new SqliteCommand(query, connection);

                command.Parameters.Add(new SqliteParameter("@name", usuario.NombreDeUsuario));

                command.Parameters.Add(new SqliteParameter("@contrasenia", usuario.Contrasenia));

                command.Parameters.Add(new SqliteParameter("@rol", usuario.Rol));

                command.ExecuteNonQuery();

                connection.Close();
            }
        }
        public void Update(int id, Usuario usuario)
        {
            var query = $"UPDATE Usuario SET nombre_de_usuario = (@name), contrasenia = (@contrasenia), rol = (@rol) WHERE id_usuario = (@id);";
            using (SqliteConnection connection = new SqliteConnection(cadenaConexion))
            {
                connection.Open();
                var command = new SqliteCommand(query, connection);

                command.Parameters.Add(new SqliteParameter("@name", usuario.NombreDeUsuario));

                command.Parameters.Add(new SqliteParameter("@contrasenia", usuario.Contrasenia));
                
                command.Parameters.Add(new SqliteParameter("@rol", usuario.Rol));

                command.Parameters.Add(new SqliteParameter("@id", id));

                command.ExecuteNonQuery();

                connection.Close();
            }
        }
        public List<Usuario> GetAll()
        {
            var queryString = @"SELECT * FROM Usuario;";
            List<Usuario> usuarios = new List<Usuario>();
            using (SqliteConnection connection = new SqliteConnection(cadenaConexion))
            {
                SqliteCommand command = new SqliteCommand(queryString, connection);
                connection.Open();

                using (SqliteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var usuario = new Usuario
                        {
                            Id = Convert.ToInt32(reader["id_usuario"]),
                            NombreDeUsuario = reader["nombre_de_usuario"].ToString(),
                            Contrasenia = reader["contrasenia"].ToString(),
                            Rol = (Rol)Convert.ToInt32(reader["rol"]) 
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
            SqliteConnection connection = new SqliteConnection(cadenaConexion);
            var usuario = new Usuario();
            SqliteCommand command = connection.CreateCommand();
            command.CommandText = $"SELECT * FROM Usuario WHERE id_usuario = @id";
            command.Parameters.Add(new SqliteParameter("@id", id));
            connection.Open();
            using (SqliteDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    usuario.Id = Convert.ToInt32(reader["id_usuario"]);
                    usuario.NombreDeUsuario = reader["nombre_de_usuario"].ToString();
                    usuario.Contrasenia = reader["contrasenia"].ToString();
                    usuario.Rol = (Rol)Convert.ToInt32(reader["rol"]);
                }
            }
            connection.Close();

            return usuario;
        }
        public void Remove(int id)
        {
            SqliteConnection connection = new SqliteConnection(cadenaConexion);
            SqliteCommand command = connection.CreateCommand();
            command.CommandText = $"DELETE FROM Usuario WHERE id_usuario = @id;";
            command.Parameters.Add(new SqliteParameter("@id", id));
            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }
    }
}