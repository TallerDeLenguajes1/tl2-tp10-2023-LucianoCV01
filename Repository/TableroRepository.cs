using Microsoft.Data.Sqlite;
using tl2_tp10_2023_LucianoCV01.Models;
namespace tl2_tp10_2023_LucianoCV01.Repository
{
    public class TableroRepository : ITableroRepository
    {
        private const string cadenaConexion = "Data Source=DB/Kanban.db;Cache=Shared";
        public List<Tablero> GetAll()
        {
            const string queryString = @"SELECT * FROM Tablero;";
            List<Tablero> tableros = new List<Tablero>();
            using (SqliteConnection connection = new SqliteConnection(cadenaConexion))
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
                            IdUsuarioPropietario = Convert.ToInt32(reader["id_usuario_propietario"]),
                            Nombre = reader["nombre"].ToString()!,
                            Descripcion = reader["descripcion"].ToString()!
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
            using (SqliteConnection connection = new SqliteConnection(cadenaConexion))
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
                            IdUsuarioPropietario = Convert.ToInt32(reader["id_usuario_propietario"]),
                            Nombre = reader["nombre"].ToString()!,
                            Descripcion = reader["descripcion"].ToString()!
                        };
                    }
                }
                connection.Close();
            };
            if (tablero == null)
            {
                throw new Exception("Tablero que se busca no existe.");
            }
            return tablero;
        }
        public List<Tablero> GetByIdUsuario(int idUsuario)
        {
            const string queryString = @"SELECT * FROM Tablero WHERE id_usuario_propietario = @propietario";
            List<Tablero> tableros = new List<Tablero>();
            using (SqliteConnection connection = new SqliteConnection(cadenaConexion))
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
                            IdUsuarioPropietario = Convert.ToInt32(reader["id_usuario_propietario"]),
                            Nombre = reader["nombre"].ToString()!,
                            Descripcion = reader["descripcion"].ToString()!
                        };
                        tableros.Add(tablero);
                    }
                }
                connection.Close();
            }
            if (tableros.Count == 0)
            {
                throw new Exception("Usuario para buscar tableros no existe.");
            }
            return tableros;
        }
        public void Create(Tablero tablero)
        {
            const string queryString = $"INSERT INTO Tablero (id_usuario_propietario, nombre, descripcion) VALUES (@usuario, @name, @descripcion)";
            using (SqliteConnection connection = new SqliteConnection(cadenaConexion))
            {
                SqliteCommand command = new SqliteCommand(queryString, connection);
                command.Parameters.Add(new SqliteParameter("@usuario", tablero.IdUsuarioPropietario));
                command.Parameters.Add(new SqliteParameter("@name", tablero.Nombre));
                command.Parameters.Add(new SqliteParameter("@descripcion", tablero.Descripcion));
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
        }
        public void Update(int id, Tablero tablero)
        {
            if (!ExisteTablero(id))
            {
                throw new Exception($"El tablero que se intenta actualizar no existe.");
            }
            const string queryString = $"UPDATE Tablero SET nombre = (@name), descripcion = (@descripcion) WHERE id = (@id);";
            using (SqliteConnection connection = new SqliteConnection(cadenaConexion))
            {
                SqliteCommand command = new SqliteCommand(queryString, connection);
                command.Parameters.Add(new SqliteParameter("@name", tablero.Nombre));
                command.Parameters.Add(new SqliteParameter("@descripcion", tablero.Descripcion));
                command.Parameters.Add(new SqliteParameter("@id", id));
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
        }
        public void Remove(int id)
        {
            if (!ExisteTablero(id))
            {
                throw new Exception($"El tablero que se intenta eliminar no existe.");
            }
            const string queryString = $"DELETE FROM Tablero WHERE id = @id;";
            using (SqliteConnection connection = new SqliteConnection(cadenaConexion))
            {
                SqliteCommand command = new SqliteCommand(queryString, connection);
                command.Parameters.Add(new SqliteParameter("@id", id));
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
        }
        private bool ExisteTablero(int id)
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