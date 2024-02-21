using Microsoft.Data.Sqlite;
using tl2_tp10_2023_LucianoCV01.Models;

namespace tl2_tp10_2023_LucianoCV01.Repository
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private string _cadenaConexion;
        public UsuarioRepository(string CadenaDeConexion)
        {
            _cadenaConexion = CadenaDeConexion;
        }
        public List<Usuario> GetAll()
        {
            const string queryString = @"SELECT * FROM Usuario;";
            List<Usuario> usuarios = new List<Usuario>();
            using (SqliteConnection connection = new SqliteConnection(_cadenaConexion))
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
                            Contrasenia = reader["contrasenia"].ToString()!,
                            Rol = (Rol)Enum.Parse(typeof(Rol), reader["rol"].ToString()!)
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
            using (SqliteConnection connection = new SqliteConnection(_cadenaConexion))
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
                            Contrasenia = reader["contrasenia"].ToString()!,
                            Rol = (Rol)Enum.Parse(typeof(Rol), reader["rol"].ToString()!)
                        };
                    }
                }
                connection.Close();
            }
            if (usuario == null)
            {
                throw new Exception("Usuario que se busca no existe.");
            }
            return usuario;
        }
        public void Create(Usuario usuario)
        {
            const string queryString = $"INSERT INTO Usuario (nombreDeUsuario, contrasenia, rol) VALUES (@name, @pass, @rol)";
            using (SqliteConnection connection = new SqliteConnection(_cadenaConexion))
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
            if (!ExisteUsuario(id))
            {
                throw new Exception($"El usuario que se intenta modificar no existe.");
            }
            const string queryString = $"UPDATE Usuario SET nombreDeUsuario = (@name), contrasenia = (@pass), rol = (@rol) WHERE id = (@id);";
            using (SqliteConnection connection = new SqliteConnection(_cadenaConexion))
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
        public void UpdateIdAsignadoTareas(int idUsuario)
        {
            if (!ExisteUsuario(idUsuario))
            {
                throw new Exception($"El usuario que se intenta de modificar sus tareas asignadas no existe.");
            }
            const string queryString = $"UPDATE Tarea SET idUsuarioAsignado = -9999 WHERE idUsuarioAsignado = @idUsuario;";
            using (SqliteConnection connection = new SqliteConnection(_cadenaConexion))
            {
                SqliteCommand command = new SqliteCommand(queryString, connection);
                command.Parameters.Add(new SqliteParameter("@idUsuario", idUsuario));
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
        }
        public void Delete(int id)
        {
            if (!ExisteUsuario(id))
            {
                throw new Exception($"El usuario que se intenta eliminar no existe.");
            }
            const string queryString = $"DELETE FROM Usuario WHERE id = @id;";
            using (SqliteConnection connection = new SqliteConnection(_cadenaConexion))
            {
                SqliteCommand command = new SqliteCommand(queryString, connection);
                command.Parameters.Add(new SqliteParameter("@id", id));
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
        }
        private bool ExisteUsuario(int id)
        {
            try
            {
                GetById(id);
                return true; // Si no lanza excepción, el tablero existe
            }
            catch (Exception)
            {
                return false; // Si lanza excepción, el tablero no existe
            }
        }
    }
}