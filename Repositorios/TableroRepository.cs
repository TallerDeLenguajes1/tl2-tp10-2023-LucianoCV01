using Microsoft.Data.Sqlite;
using tl2_tp10_2023_LucianoCV01.Models;

namespace EspacioRepositorios
{
    class TableroRepository : ITableroRepository
    {
        private string cadenaConexion = "Data Source=DB/TP08-CosentinoLuciano.db;Cache=Shared";

        public void Create(Tablero tablero) // no recibe id_usuario por ser FK
        {
            var query = $"INSERT INTO Tablero (id_usuario_propietario, nombre, descripcion) VALUES (@usuario, @name, @descripcion)";
            using (SqliteConnection connection = new SqliteConnection(cadenaConexion))
            {

                connection.Open();
                var command = new SqliteCommand(query, connection);

                command.Parameters.Add(new SqliteParameter("@usuario", tablero.IdUsuarioPropietario));
                command.Parameters.Add(new SqliteParameter("@name", tablero.Nombre));
                command.Parameters.Add(new SqliteParameter("@descripcion", tablero.Descripcion));


                command.ExecuteNonQuery();

                connection.Close();
            }
        }
        public void Update(int id, Tablero tablero)
        {
            var query = $"UPDATE Tablero SET id_usuario_propietario = (@propietario), nombre = (@name), descripcion = (@descripcion) WHERE id_tablero = (@id);";
            using (SqliteConnection connection = new SqliteConnection(cadenaConexion))
            {
                connection.Open();
                var command = new SqliteCommand(query, connection);

                command.Parameters.Add(new SqliteParameter("@propietario", tablero.IdUsuarioPropietario));

                command.Parameters.Add(new SqliteParameter("@name", tablero.Nombre));

                command.Parameters.Add(new SqliteParameter("@descripcion", tablero.Descripcion));

                command.Parameters.Add(new SqliteParameter("@id", id));

                command.ExecuteNonQuery();

                connection.Close();
            }
        }
        public Tablero GetById(int id)
        {
            SqliteConnection connection = new SqliteConnection(cadenaConexion);
            var tablero = new Tablero();
            SqliteCommand command = connection.CreateCommand();
            command.CommandText = $"SELECT * FROM Tablero WHERE id_tablero = @id";
            command.Parameters.Add(new SqliteParameter("@id", id));
            connection.Open();
            using (SqliteDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    tablero.Id = Convert.ToInt32(reader["id_tablero"]);
                    tablero.IdUsuarioPropietario = Convert.ToInt32(reader["id_usuario_propietario"]);
                    tablero.Nombre = reader["nombre"].ToString();
                    tablero.Descripcion = reader["descripcion"].ToString();
                }
            }
            connection.Close();

            return tablero;
        }
        public List<Tablero> GetAll()
        {
            var queryString = @"SELECT * FROM Tablero;";
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
                            Id = Convert.ToInt32(reader["id_tablero"]),
                            IdUsuarioPropietario = Convert.ToInt32(reader["id_usuario_propietario"]),
                            Nombre = reader["nombre"].ToString(),
                            Descripcion = reader["descripcion"].ToString()
                        };
                        tableros.Add(tablero);
                    }
                }
                connection.Close();
            }
            return tableros;
        }
        public List<Tablero> GetByIdUsuario(int idUsuario)
        {
            SqliteConnection connection = new SqliteConnection(cadenaConexion);
            List<Tablero> tableros = new List<Tablero>();
            SqliteCommand command = connection.CreateCommand();
            command.CommandText = $"SELECT * FROM Tablero WHERE id_usuario_propietario = @propietario";
            command.Parameters.Add(new SqliteParameter("@id", idUsuario));
            connection.Open();
            using (SqliteDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    var tablero = new Tablero
                    {
                        Id = Convert.ToInt32(reader["id_tablero"]),
                        IdUsuarioPropietario = Convert.ToInt32(reader["id_usuario_propietario"]),
                        Nombre = reader["nombre"].ToString(),
                        Descripcion = reader["descripcion"].ToString()
                    };
                    tableros.Add(tablero);
                }
            }
            connection.Close();

            return tableros;
        }
        public void Remove(int id)
        {
            SqliteConnection connection = new SqliteConnection(cadenaConexion);
            SqliteCommand command = connection.CreateCommand();
            command.CommandText = $"DELETE FROM Tablero WHERE id_tablero = @id;";
            command.Parameters.Add(new SqliteParameter("@id", id));
            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }
    }
}