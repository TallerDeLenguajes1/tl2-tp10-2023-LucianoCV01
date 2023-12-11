using Microsoft.Data.Sqlite;
using tl2_tp10_2023_LucianoCV01.Models;
namespace tl2_tp10_2023_LucianoCV01.Repository
{
    class TareaRepository : ITareaRepository
    {
        private string _cadenaConexion;
        public TareaRepository(string CadenaDeConexion)
        {
            _cadenaConexion = CadenaDeConexion;
        }
        public List<Tarea> GetAll()
        {
            const string queryString = @"SELECT * FROM Tarea;";
            List<Tarea> tareas = new List<Tarea>();
            int usuarioAsignado;
            using (SqliteConnection connection = new SqliteConnection(_cadenaConexion))
            {
                SqliteCommand command = new SqliteCommand(queryString, connection);
                connection.Open();
                using (SqliteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        usuarioAsignado = -9999;
                        if (reader["id_usuario_asignado"] != DBNull.Value)
                        {
                            usuarioAsignado = Convert.ToInt32(reader["id_usuario_asignado"]);
                        }
                        var tarea = new Tarea
                        {
                            Id = Convert.ToInt32(reader["id"]),
                            IdTablero = Convert.ToInt32(reader["id_tablero"]),
                            Nombre = reader["nombre"].ToString()!,
                            Estado = (Estado)Convert.ToInt32(reader["estado"]),
                            Descripcion = reader["descripcion"].ToString()!,
                            Color = reader["color"].ToString()!,
                            IdUsuarioAsignado = usuarioAsignado
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
                            IdTablero = Convert.ToInt32(reader["id_tablero"]),
                            Nombre = reader["nombre"].ToString()!,
                            Estado = (Estado)Convert.ToInt32(reader["estado"]),
                            Descripcion = reader["descripcion"].ToString()!,
                            Color = reader["color"].ToString()!
                        };
                        if (reader["id_usuario_asignado"] != DBNull.Value)
                        {
                            tarea.IdUsuarioAsignado = Convert.ToInt32(reader["id_usuario_asignado"]);
                        }
                        else
                        {
                            tarea.IdUsuarioAsignado = -9999; // Modificar para mostrar que no esta asignado a ningun usuario
                        }
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
            const string queryString = @"SELECT * FROM Tarea WHERE id_usuario_asignado = @asignado";
            List<Tarea> tareas = new List<Tarea>();
            int usuarioAsignado;
            using (SqliteConnection connection = new SqliteConnection(_cadenaConexion))
            {
                SqliteCommand command = new SqliteCommand(queryString, connection);
                command.Parameters.Add(new SqliteParameter("@asignado", idUsuario));
                connection.Open();
                using (SqliteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        usuarioAsignado = -9999;
                        if (reader["id_usuario_asignado"] != DBNull.Value)
                        {
                            usuarioAsignado = Convert.ToInt32(reader["id_usuario_asignado"]);
                        }
                        var tarea = new Tarea
                        {
                            Id = Convert.ToInt32(reader["id"]),
                            IdTablero = Convert.ToInt32(reader["id_tablero"]),
                            Nombre = reader["nombre"].ToString()!,
                            Estado = (Estado)Convert.ToInt32(reader["estado"]),
                            Descripcion = reader["descripcion"].ToString()!,
                            Color = reader["color"].ToString()!,
                            IdUsuarioAsignado = usuarioAsignado
                        };
                        tareas.Add(tarea);
                    }
                }
                connection.Close();
            }
            if (tareas.Count == 0)
            {
                throw new Exception("Usuario para buscar tareas no existe.");
            }
            return tareas;
        }
        public List<Tarea> GetByIdTablero(int idTablero)
        {
            const string queryString = @"SELECT * FROM Tarea WHERE id_tablero = @tablero";
            List<Tarea> tareas = new List<Tarea>();
            int usuarioAsignado;
            using (SqliteConnection connection = new SqliteConnection(_cadenaConexion))
            {
                SqliteCommand command = new SqliteCommand(queryString, connection);
                command.Parameters.Add(new SqliteParameter("@tablero", idTablero));
                connection.Open();
                using (SqliteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        usuarioAsignado = -9999;
                        if (reader["id_usuario_asignado"] != DBNull.Value)
                        {
                            usuarioAsignado = Convert.ToInt32(reader["id_usuario_asignado"]);
                        }
                        var tarea = new Tarea
                        {
                            Id = Convert.ToInt32(reader["id"]),
                            IdTablero = Convert.ToInt32(reader["id_tablero"]),
                            Nombre = reader["nombre"].ToString()!,
                            Estado = (Estado)Convert.ToInt32(reader["estado"]),
                            Descripcion = reader["descripcion"].ToString()!,
                            Color = reader["color"].ToString()!,
                            IdUsuarioAsignado = usuarioAsignado
                        };
                        tareas.Add(tarea);
                    }
                }
                connection.Close();
            }
            if (tareas.Count == 0)
            {
                throw new Exception("Tablero para buscar tareas no existe.");
            }
            return tareas;
        }
        public void Create(int idTablero, Tarea tarea)
        {
            const string queryString = $"INSERT INTO Tarea (id_tablero, nombre, estado, descripcion, color, id_usuario_asignado) VALUES (@tablero, @name, @estado, @descripcion, @color, @usuario)";
            using (SqliteConnection connection = new SqliteConnection(_cadenaConexion))
            {
                SqliteCommand command = new SqliteCommand(queryString, connection);
                command.Parameters.Add(new SqliteParameter("@tablero", idTablero));
                command.Parameters.Add(new SqliteParameter("@name", tarea.Nombre));
                command.Parameters.Add(new SqliteParameter("@estado", (int)tarea.Estado));
                command.Parameters.Add(new SqliteParameter("@descripcion", tarea.Descripcion));
                command.Parameters.Add(new SqliteParameter("@color", tarea.Color));
                if (tarea.IdUsuarioAsignado == -9999) // Cambiar
                {
                    command.Parameters.Add(new SqliteParameter("@usuario", DBNull.Value));
                }
                else
                {
                    command.Parameters.Add(new SqliteParameter("@usuario", tarea.IdUsuarioAsignado));
                }
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
            const string queryString = $"UPDATE Tarea SET nombre = (@name), estado = (@estado), descripcion = (@descripcion), color = (@color), id_usuario_asignado = (@usuario) WHERE id = (@id);";
            using (SqliteConnection connection = new SqliteConnection(_cadenaConexion))
            {
                SqliteCommand command = new SqliteCommand(queryString, connection);
                command.Parameters.Add(new SqliteParameter("@name", tarea.Nombre));
                command.Parameters.Add(new SqliteParameter("@estado", tarea.Estado));
                command.Parameters.Add(new SqliteParameter("@descripcion", tarea.Descripcion));
                command.Parameters.Add(new SqliteParameter("@color", tarea.Color));
                if (tarea.IdUsuarioAsignado == -9999) // Cambiar
                {
                    command.Parameters.Add(new SqliteParameter("@usuario", DBNull.Value));
                }
                else
                {
                    command.Parameters.Add(new SqliteParameter("@usuario", tarea.IdUsuarioAsignado));
                }
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
            const string queryString = $"UPDATE Tarea SET id_usuario_asignado = (@usuario) WHERE id = (@id);";
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
        public void Remove(int id)
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