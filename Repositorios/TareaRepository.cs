using Microsoft.Data.Sqlite;
using tl2_tp10_2023_LucianoCV01.Models;

namespace EspacioRepositorios
{
    class TareaRepository : ITareaRepository
    {
        private string cadenaConexion = "Data Source=DB/TP08-CosentinoLuciano.db;Cache=Shared";

        public void Create(int idTablero, Tarea tarea) // Mejorar para que se pueda crear sin id_usuario
        {
            var query = $"INSERT INTO Tarea (id_tablero, nombre, estado, descripcion, color, id_usuario_asignado) VALUES (@tablero, @name, @estado, @descripcion, @color, @usuario)";
            using (SqliteConnection connection = new SqliteConnection(cadenaConexion))
            {

                connection.Open();
                var command = new SqliteCommand(query, connection);

                command.Parameters.Add(new SqliteParameter("@tablero", idTablero));
                command.Parameters.Add(new SqliteParameter("@name", tarea.Nombre));
                command.Parameters.Add(new SqliteParameter("@estado", (int)tarea.Estado)); 
                command.Parameters.Add(new SqliteParameter("@descripcion", tarea.Descripcion));
                command.Parameters.Add(new SqliteParameter("@color", tarea.Color));
                if (tarea.IdUsuarioAsignado == null)
                {
                    command.Parameters.Add(new SqliteParameter("@usuario", DBNull.Value));
                } else
                {
                    command.Parameters.Add(new SqliteParameter("@usuario", tarea.IdUsuarioAsignado));
                }

                command.ExecuteNonQuery();

                connection.Close();
            }
        }
        public void Update(int id, Tarea tarea)
        {
            var query = $"UPDATE Tarea SET id_tablero = (@tablero), nombre = (@name), estado = (@estado), descripcion = (@descripcion), color = (@color), id_usuario_asignado = (@usuario) WHERE id_tarea = (@id);";
            using (SqliteConnection connection = new SqliteConnection(cadenaConexion))
            {
                connection.Open();
                var command = new SqliteCommand(query, connection);

                command.Parameters.Add(new SqliteParameter("@tablero", tarea.IdTablero));

                command.Parameters.Add(new SqliteParameter("@name", tarea.Nombre));

                command.Parameters.Add(new SqliteParameter("@estado", tarea.Estado));

                command.Parameters.Add(new SqliteParameter("@descripcion", tarea.Descripcion));

                command.Parameters.Add(new SqliteParameter("@color", tarea.Color));

                if (tarea.IdUsuarioAsignado == null)
                {
                    command.Parameters.Add(new SqliteParameter("@usuario", DBNull.Value));
                } else
                {
                    command.Parameters.Add(new SqliteParameter("@usuario", tarea.IdUsuarioAsignado));
                }

                command.Parameters.Add(new SqliteParameter("@id", id));

                command.ExecuteNonQuery();

                connection.Close();
            }
        }
        public Tarea GetById(int id)
        {
            SqliteConnection connection = new SqliteConnection(cadenaConexion);
            var tarea = new Tarea();
            SqliteCommand command = connection.CreateCommand();
            command.CommandText = $"SELECT * FROM Tarea WHERE id_tarea = @id";
            command.Parameters.Add(new SqliteParameter("@id", id));
            connection.Open();
            using (SqliteDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    tarea.Id = Convert.ToInt32(reader["id_tarea"]);
                    tarea.IdTablero = Convert.ToInt32(reader["id_tablero"]);
                    tarea.Nombre = reader["nombre"].ToString();
                    tarea.Estado = (EstadoTarea)Enum.ToObject(typeof(EstadoTarea), Convert.ToInt32(reader["estado"]));
                    tarea.Descripcion = reader["descripcion"].ToString();
                    tarea.Color = reader["color"].ToString();
                    if (reader["id_usuario_asignado"] != DBNull.Value)
                    {
                        tarea.IdUsuarioAsignado = Convert.ToInt32(reader["id_usuario_asignado"]);
                    } else
                    {
                        tarea.IdUsuarioAsignado = null;  
                    }
                }
            }
            connection.Close();

            return tarea;
        }
        public List<Tarea> GetByIdUsuario(int idUsuario)
        {
            SqliteConnection connection = new SqliteConnection(cadenaConexion);
            List<Tarea> tareas = new List<Tarea>();
            SqliteCommand command = connection.CreateCommand();
            command.CommandText = $"SELECT * FROM Tarea WHERE id_usuario_asignado = @asignado";
            command.Parameters.Add(new SqliteParameter("@asignado", idUsuario));
            connection.Open();
            using (SqliteDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    var tarea = new Tarea
                    {
                        Id = Convert.ToInt32(reader["id_tarea"]),
                        IdTablero = Convert.ToInt32(reader["id_tablero"]),
                        Nombre = reader["nombre"].ToString(),
                        Estado = (EstadoTarea)Enum.ToObject(typeof(EstadoTarea), Convert.ToInt32(reader["estado"])), 
                        Descripcion = reader["descripcion"].ToString(),
                        Color = reader["color"].ToString(),
                        IdUsuarioAsignado = Convert.ToInt32(reader["id_usuario_asignado"])
                    };
                    tareas.Add(tarea);
                }
            }
            connection.Close();

            return tareas;
        }
        public List<Tarea> GetByIdTablero(int idTablero)
        {
            SqliteConnection connection = new SqliteConnection(cadenaConexion);
            List<Tarea> tareas = new List<Tarea>();
            SqliteCommand command = connection.CreateCommand();
            command.CommandText = $"SELECT * FROM Tarea WHERE id_tablero = @tablero";
            command.Parameters.Add(new SqliteParameter("@tablero", idTablero));
            connection.Open();
            using (SqliteDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    int usuarioAsignado = -9999;
                    if (reader["id_usuario_asignado"] != DBNull.Value)
                    {
                        usuarioAsignado = Convert.ToInt32(reader["id_usuario_asignado"]);
                    }
                    var tarea = new Tarea
                    {
                        Id = Convert.ToInt32(reader["id_tarea"]),
                        IdTablero = Convert.ToInt32(reader["id_tablero"]),
                        Nombre = reader["nombre"].ToString(),
                        Estado = (EstadoTarea)Enum.ToObject(typeof(EstadoTarea), Convert.ToInt32(reader["estado"])), 
                        Descripcion = reader["descripcion"].ToString(),
                        Color = reader["color"].ToString(),
                        IdUsuarioAsignado = usuarioAsignado
                    };
                    tareas.Add(tarea);
                }
            }
            connection.Close();

            return tareas;
        }
        public void Remove(int id)
        {
            SqliteConnection connection = new SqliteConnection(cadenaConexion);
            SqliteCommand command = connection.CreateCommand();
            command.CommandText = $"DELETE FROM Tarea WHERE id_tarea = @id;";
            command.Parameters.Add(new SqliteParameter("@id", id));
            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }
        public void AsignarUsuario(int idTarea, int idUsuario)
        {
            var query = $"UPDATE Tarea SET id_usuario_asignado = (@usuario) WHERE id_tarea = (@id);";
            using (SqliteConnection connection = new SqliteConnection(cadenaConexion))
            {
                connection.Open();
                var command = new SqliteCommand(query, connection);

                command.Parameters.Add(new SqliteParameter("@usuario", idUsuario));

                command.Parameters.Add(new SqliteParameter("@id", idTarea));

                command.ExecuteNonQuery();

                connection.Close();
            }
        }
        public List<Tarea> GetByEstado(int estado)
        {
            SqliteConnection connection = new SqliteConnection(cadenaConexion);
            List<Tarea> tareas = new List<Tarea>();
            SqliteCommand command = connection.CreateCommand();
            command.CommandText = $"SELECT * FROM Tarea WHERE estado = @estado";
            command.Parameters.Add(new SqliteParameter("@estado", estado));
            connection.Open();
            using (SqliteDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    var tarea = new Tarea
                    {
                        Id = Convert.ToInt32(reader["id_tarea"]),
                        IdTablero = Convert.ToInt32(reader["id_tablero"]),
                        Nombre = reader["nombre"].ToString(),
                        Estado = (EstadoTarea)Enum.ToObject(typeof(EstadoTarea), Convert.ToInt32(reader["estado"])), 
                        Descripcion = reader["descripcion"].ToString(),
                        Color = reader["color"].ToString(),
                        IdUsuarioAsignado = Convert.ToInt32(reader["id_usuario_asignado"])
                    };
                    tareas.Add(tarea);
                }
            }
            connection.Close();

            return tareas;
        }
    }
}