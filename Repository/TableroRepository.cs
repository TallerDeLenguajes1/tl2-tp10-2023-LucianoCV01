using Microsoft.Data.Sqlite;
using tl2_tp10_2023_LucianoCV01.Models;

namespace tl2_tp10_2023_LucianoCV01.Repository
{
    public class TableroRepository : ITableroRepository
    {
        private string _cadenaConexion;
        public TableroRepository(string CadenaDeConexion)
        {
            _cadenaConexion = CadenaDeConexion;
        }
        public List<Tablero> GetAll()
        {
            const string queryString = @"SELECT * FROM Tablero;";
            List<Tablero> tableros = new List<Tablero>();
            using (SqliteConnection connection = new SqliteConnection(_cadenaConexion))
            {
                SqliteCommand command = new SqliteCommand(queryString, connection);
                connection.Open();
                using (SqliteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var tablero = new Tablero
                        {
                            Id = Convert.ToInt32(reader["id"]),
                            IdUsuarioPropietario = Convert.ToInt32(reader["idUsuarioPropietario"]),
                            Nombre = reader["nombre"].ToString()!,
                            Descripcion = reader["descripcion"].ToString()
                        };
                        tableros.Add(tablero);
                    }
                }
                connection.Close();
            }
            return tableros;
        }
        public Tablero GetById(int id)
        {
            const string queryString = $"SELECT * FROM Tablero WHERE id = @id";
            Tablero? tablero = null;
            using (SqliteConnection connection = new SqliteConnection(_cadenaConexion))
            {
                SqliteCommand command = new SqliteCommand(queryString, connection);
                command.Parameters.Add(new SqliteParameter("@id", id));
                connection.Open();
                using (SqliteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        tablero = new Tablero
                        {
                            Id = Convert.ToInt32(reader["id"]),
                            IdUsuarioPropietario = Convert.ToInt32(reader["idUsuarioPropietario"]),
                            Nombre = reader["nombre"].ToString()!,
                            Descripcion = reader["descripcion"].ToString()
                        };
                    }
                }
                connection.Close();
            };
            return tablero; // lanzar excepcion si el tablero que se busca no existe
        }
        public List<Tablero> GetByIdUsuario(int idUsuario)
        {
            // lanzar excepcion si el usuario propietario no existe
            // si existe puede que tenga tableros relacionados como no.
            const string queryString = @"SELECT * FROM Tablero WHERE idUsuarioPropietario = @propietario";
            List<Tablero> tableros = new List<Tablero>();
            using (SqliteConnection connection = new SqliteConnection(_cadenaConexion))
            {
                SqliteCommand command = new SqliteCommand(queryString, connection);
                command.Parameters.Add(new SqliteParameter("@propietario", idUsuario));
                connection.Open();
                using (SqliteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var tablero = new Tablero
                        {
                            Id = Convert.ToInt32(reader["id"]),
                            IdUsuarioPropietario = Convert.ToInt32(reader["idUsuarioPropietario"]),
                            Nombre = reader["nombre"].ToString()!,
                            Descripcion = reader["descripcion"].ToString()
                        };
                        tableros.Add(tablero);
                    }
                }
                connection.Close();
            }
            return tableros;
        }
        public List<Tablero> GetByIdUsuarioParticipante(int idUsuario)
        {
            // lanzar excepcion si el usuario propietario no existe
            // si existe puede que tenga tableros relacionados como no.
            const string queryString = @"SELECT Tablero.id, Tablero.idUsuarioPropietario, Tablero.nombre, Tablero.descripcion 
FROM Tablero 
INNER JOIN Tarea ON Tablero.id = Tarea.idTablero 
WHERE Tarea.idUsuarioAsignado = @participante 
AND Tablero.idUsuarioPropietario != @participante;
";
            List<Tablero> tableros = new List<Tablero>();
            using (SqliteConnection connection = new SqliteConnection(_cadenaConexion))
            {
                SqliteCommand command = new SqliteCommand(queryString, connection);
                command.Parameters.Add(new SqliteParameter("@participante", idUsuario));
                connection.Open();
                using (SqliteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var tablero = new Tablero
                        {
                            Id = Convert.ToInt32(reader["id"]),
                            IdUsuarioPropietario = Convert.ToInt32(reader["idUsuarioPropietario"]),
                            Nombre = reader["nombre"].ToString()!,
                            Descripcion = reader["descripcion"].ToString()
                        };
                        tableros.Add(tablero);
                    }
                }
                connection.Close();
            }
            return tableros;
        }
        public List<Tablero> GetByIdUsuarioAjenos(int idUsuario)
        {
            // lanzar excepcion si el usuario propietario no existe
            // si existe puede que tenga tableros relacionados como no.
            const string queryString = @"SELECT Tablero.id, Tablero.idUsuarioPropietario, Tablero.nombre, Tablero.descripcion FROM Tablero LEFT JOIN Tarea ON Tablero.id = Tarea.idTablero AND Tarea.idUsuarioAsignado = @ajeno WHERE Tablero.idUsuarioPropietario != @ajeno AND Tarea.idTablero IS NULL;";
            List<Tablero> tableros = new List<Tablero>();
            using (SqliteConnection connection = new SqliteConnection(_cadenaConexion))
            {
                SqliteCommand command = new SqliteCommand(queryString, connection);
                command.Parameters.Add(new SqliteParameter("@ajeno", idUsuario));
                connection.Open();
                using (SqliteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var tablero = new Tablero
                        {
                            Id = Convert.ToInt32(reader["id"]),
                            IdUsuarioPropietario = Convert.ToInt32(reader["idUsuarioPropietario"]),
                            Nombre = reader["nombre"].ToString()!,
                            Descripcion = reader["descripcion"].ToString()
                        };
                        tableros.Add(tablero);
                    }
                }
                connection.Close();
            }
            return tableros;
        }
        public List<Usuario> GetByIdTableroSusParticipantes(int idTablero)
        {
            // lanzar excepcion si el usuario propietario no existe
            // si existe puede que tenga tableros relacionados como no.
            const string queryString = @"SELECT U.id FROM Usuario U INNER JOIN Tablero T ON U.id = T.idUsuarioPropietario WHERE T.id = @idTablero UNION SELECT DISTINCT U2.id FROM Usuario U2 INNER JOIN Tarea T2 ON U2.id = T2.idUsuarioAsignado WHERE T2.idTablero = @idTablero;";
            List<Usuario> usuarios = new List<Usuario>();
            using (SqliteConnection connection = new SqliteConnection(_cadenaConexion))
            {
                SqliteCommand command = new SqliteCommand(queryString, connection);
                command.Parameters.Add(new SqliteParameter("@idTablero", idTablero));
                connection.Open();
                using (SqliteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var usuario = new Usuario
                        {
                            Id = Convert.ToInt32(reader["id"]),
                        };
                        usuarios.Add(usuario);
                    }
                }
                connection.Close();
            }
            return usuarios;
        }
        public void Create(Tablero tablero)
        {
            const string queryString = $"INSERT INTO Tablero (idUsuarioPropietario, nombre, descripcion) VALUES (@usuario, @name, @descripcion)";
            using (SqliteConnection connection = new SqliteConnection(_cadenaConexion))
            {
                SqliteCommand command = new SqliteCommand(queryString, connection);
                command.Parameters.Add(new SqliteParameter("@usuario", tablero.IdUsuarioPropietario));
                command.Parameters.Add(new SqliteParameter("@name", tablero.Nombre));
                command.Parameters.Add(new SqliteParameter("@descripcion", tablero.Descripcion != null ? tablero.Descripcion : ""));
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
        }
        public void Update(int id, Tablero tablero)
        {
            // lanzar excepcion si el tablero que quiero actualizar no existe.
            const string queryString = $"UPDATE Tablero SET idUsuarioPropietario = (@usuario), nombre = (@name), descripcion = (@descripcion) WHERE id = (@id);";
            using (SqliteConnection connection = new SqliteConnection(_cadenaConexion))
            {
                SqliteCommand command = new SqliteCommand(queryString, connection);
                command.Parameters.Add(new SqliteParameter("@usuario", tablero.IdUsuarioPropietario));
                command.Parameters.Add(new SqliteParameter("@name", tablero.Nombre));
                command.Parameters.Add(new SqliteParameter("@descripcion", tablero.Descripcion));
                command.Parameters.Add(new SqliteParameter("@id", id));
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
        }
        public void Delete(int id)
        {
            // lanzar excepcion si el tablero a eliminar no existe.
            const string queryString = $"DELETE FROM Tablero WHERE id = @id;";
            using (SqliteConnection connection = new SqliteConnection(_cadenaConexion))
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