using Microsoft.Data.Sqlite;
using tl2_tp10_2023_LucianoCV01.Models;
namespace tl2_tp10_2023_LucianoCV01.Repository
{
    class TareaRepository : ITareaRepository
    {
        private const int idUsuarioAsignadoDefecto = -9999;
        private string _cadenaConexion;
        public TareaRepository(string CadenaDeConexion)
        {
            _cadenaConexion = CadenaDeConexion;
        }
        public List<Tarea> GetAll()
        {
            const string queryString = @"SELECT * FROM Tarea;";
            List<Tarea> tareas = new List<Tarea>();
            using (SqliteConnection connection = new SqliteConnection(_cadenaConexion))
            {
                SqliteCommand command = new SqliteCommand(queryString, connection);
                connection.Open();
                using (SqliteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var tarea = new Tarea
                        {
                            Id = Convert.ToInt32(reader["id"]),
                            IdTablero = Convert.ToInt32(reader["idTablero"]),
                            Nombre = reader["nombre"].ToString()!,
                            Estado = (EstadoTarea)Enum.Parse(typeof(EstadoTarea), reader["estado"].ToString()!),
                            Descripcion = reader["descripcion"].ToString(),
                            Color = (ColorEtiqueta)Enum.Parse(typeof(ColorEtiqueta), reader["color"].ToString()!),
                            IdUsuarioAsignado = Convert.ToInt32(reader["idUsuarioAsignado"]),
                        };
                        tareas.Add(tarea);
                    }
                }
                connection.Close();
            }
            return tareas;
        }
        public Tarea GetById(int id)
        {
            const string queryString = $"SELECT * FROM Tarea WHERE id = @id";
            Tarea? tarea = null;
            using (SqliteConnection connection = new SqliteConnection(_cadenaConexion))
            {
                SqliteCommand command = new SqliteCommand(queryString, connection);
                command.Parameters.Add(new SqliteParameter("@id", id));
                connection.Open();
                using (SqliteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        tarea = new Tarea
                        {
                            Id = Convert.ToInt32(reader["id"]),
                            IdTablero = Convert.ToInt32(reader["idTablero"]),
                            Nombre = reader["nombre"].ToString()!,
                            Estado = (EstadoTarea)Enum.Parse(typeof(EstadoTarea), reader["estado"].ToString()!),
                            Descripcion = reader["descripcion"].ToString(),
                            Color = (ColorEtiqueta)Enum.Parse(typeof(ColorEtiqueta), reader["color"].ToString()!),
                            IdUsuarioAsignado = Convert.ToInt32(reader["idUsuarioAsignado"]),
                        };
                    }
                }
                connection.Close();
            };
            if (tarea == null)
            {
                throw new Exception("Tarea que se busca no existe.");
            }
            return tarea;
        }
        public List<Tarea> GetByIdUsuario(int idUsuario)
        {
            // lanzar excepcion si el usuario al cual se le busca sus tareas no existe. Puede existir y no tener tareas asociadas.
            const string queryString = @"SELECT * FROM Tarea WHERE idUsuarioAsignado = @asignado";
            List<Tarea> tareas = new List<Tarea>();
            using (SqliteConnection connection = new SqliteConnection(_cadenaConexion))
            {
                SqliteCommand command = new SqliteCommand(queryString, connection);
                command.Parameters.Add(new SqliteParameter("@asignado", idUsuario));
                connection.Open();
                using (SqliteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var tarea = new Tarea
                        {
                            Id = Convert.ToInt32(reader["id"]),
                            IdTablero = Convert.ToInt32(reader["idTablero"]),
                            Nombre = reader["nombre"].ToString()!,
                            Estado = (EstadoTarea)Enum.Parse(typeof(EstadoTarea), reader["estado"].ToString()!),
                            Descripcion = reader["descripcion"].ToString(),
                            Color = (ColorEtiqueta)Enum.Parse(typeof(ColorEtiqueta), reader["color"].ToString()!),
                            IdUsuarioAsignado = Convert.ToInt32(reader["idUsuarioAsignado"]),
                        };
                        tareas.Add(tarea);
                    }
                }
                connection.Close();
            }
            return tareas;
        }
        public List<Tarea> GetByIdTablero(int idTablero)
        {
            // lanzar excepcion si no existe el tablero asociado a las tareas.
            const string queryString = @"SELECT * FROM Tarea WHERE idTablero = @tablero";
            List<Tarea> tareas = new List<Tarea>();
            using (SqliteConnection connection = new SqliteConnection(_cadenaConexion))
            {
                SqliteCommand command = new SqliteCommand(queryString, connection);
                command.Parameters.Add(new SqliteParameter("@tablero", idTablero));
                connection.Open();
                using (SqliteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var tarea = new Tarea
                        {
                            Id = Convert.ToInt32(reader["id"]),
                            IdTablero = Convert.ToInt32(reader["idTablero"]),
                            Nombre = reader["nombre"].ToString()!,
                            Estado = (EstadoTarea)Enum.Parse(typeof(EstadoTarea), reader["estado"].ToString()!),
                            Descripcion = reader["descripcion"].ToString(),
                            Color = (ColorEtiqueta)Enum.Parse(typeof(ColorEtiqueta), reader["color"].ToString()!),
                            IdUsuarioAsignado = Convert.ToInt32(reader["idUsuarioAsignado"]),
                        };
                        tareas.Add(tarea);
                    }
                }
                connection.Close();
            }
            return tareas;
        }
        public void Create(int idTablero, Tarea tarea)
        {
            const string queryString = $"INSERT INTO Tarea (idTablero, nombre, estado, descripcion, color, idUsuarioAsignado) VALUES (@tablero, @name, @estado, @descripcion, @color, @usuario)";
            using (SqliteConnection connection = new SqliteConnection(_cadenaConexion))
            {
                SqliteCommand command = new SqliteCommand(queryString, connection);
                command.Parameters.Add(new SqliteParameter("@tablero", idTablero));
                command.Parameters.Add(new SqliteParameter("@name", tarea.Nombre));
                command.Parameters.Add(new SqliteParameter("@estado", Convert.ToInt32(tarea.Estado)));
                command.Parameters.Add(new SqliteParameter("@descripcion", tarea.Descripcion != null ? tarea.Descripcion : ""));
                command.Parameters.Add(new SqliteParameter("@color", Convert.ToInt32(tarea.Color)));
                command.Parameters.Add(new SqliteParameter("@usuario", tarea.IdUsuarioAsignado != null ? tarea.IdUsuarioAsignado : idUsuarioAsignadoDefecto));

                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
        }
        public void Update(int id, Tarea tarea)
        {
            if (!ExisteTarea(id))
            {
                throw new Exception($"La tarea que se intenta actualizar no existe.");
            }
            const string queryString = $"UPDATE Tarea SET nombre = (@name), estado = (@estado), descripcion = (@descripcion), color = (@color), idUsuarioAsignado = (@asignado) WHERE id = (@id);";
            using (SqliteConnection connection = new SqliteConnection(_cadenaConexion))
            {
                SqliteCommand command = new SqliteCommand(queryString, connection);
                command.Parameters.Add(new SqliteParameter("@name", tarea.Nombre));
                command.Parameters.Add(new SqliteParameter("@estado", Convert.ToInt32(tarea.Estado)));
                command.Parameters.Add(new SqliteParameter("@descripcion", tarea.Descripcion != null ? tarea.Descripcion : ""));
                command.Parameters.Add(new SqliteParameter("@color", Convert.ToInt32(tarea.Color)));
                command.Parameters.Add(new SqliteParameter("@asignado", Convert.ToInt32(tarea.IdUsuarioAsignado)));
                command.Parameters.Add(new SqliteParameter("@id", id));
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
        }
        public void UpdateUsuario(int id, int idUsuario)
        {
            if (!ExisteTarea(id))
            {
                throw new Exception($"La tarea a la que se le intenta actualizar el usuario no existe.");
            }
            const string queryString = $"UPDATE Tarea SET idUsuarioAsignado = (@usuario) WHERE id = (@id);";
            using (SqliteConnection connection = new SqliteConnection(_cadenaConexion))
            {
                SqliteCommand command = new SqliteCommand(queryString, connection);
                command.Parameters.Add(new SqliteParameter("@usuario", idUsuario));
                command.Parameters.Add(new SqliteParameter("@id", id));
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
        }
        public void Delete(int id)
        {
            if (!ExisteTarea(id))
            {
                throw new Exception($"La tarea que se intenta eliminar no existe.");
            }
            const string queryString = $"DELETE FROM Tarea WHERE id = @id;";
            using (SqliteConnection connection = new SqliteConnection(_cadenaConexion))
            {
                SqliteCommand command = new SqliteCommand(queryString, connection);
                command.Parameters.Add(new SqliteParameter("@id", id));
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
        }
        private bool ExisteTarea(int id)
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