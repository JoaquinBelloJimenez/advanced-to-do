using System.Data;
using Microsoft.Data.SqlClient;
using TodoListApi.Models;

namespace TodoListApi.Repositories
{
    public class TaskRepository(IConfiguration configuration)
    {
#pragma warning disable CS8601 // Posible asignación de referencia nula
        private readonly string _connectionString = configuration.GetConnectionString("DefaultConnection");
#pragma warning restore CS8601 // Posible asignación de referencia nula

        public async Task<List<UserTask>> GetAllTasksAsync()
        {
            var tasks = new List<UserTask>();
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand("GetAllTasks", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            tasks.Add(new UserTask
                            {
                                Id = reader.GetInt32(0),
                                Title = reader.GetString(1),
                                Description = reader.IsDBNull(2) ? null : reader.GetString(2),
                                IsCompleted = reader.GetBoolean(3),
                                CreatedAt = reader.GetDateTime(4),
                                UpdatedAt = reader.GetDateTime(5)
                            });
                        }
                    }
                }
            }
            return tasks;
        }

        public async Task<UserTask> GetTaskByIdAsync(int id)
        {
            UserTask task = null;
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand("GetTaskById", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Id", id);
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            task = new UserTask
                            {
                                Id = reader.GetInt32(0),
                                Title = reader.GetString(1),
                                Description = reader.IsDBNull(2) ? null : reader.GetString(2),
                                IsCompleted = reader.GetBoolean(3),
                                CreatedAt = reader.GetDateTime(4),
                                UpdatedAt = reader.GetDateTime(5)
                            };
                        }
                    }
                }
            }
            return task;
        }

        public async Task<int> CreateTaskAsync(UserTask task)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand("CreateTask", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Title", task.Title);
                    command.Parameters.AddWithValue("@Description", task.Description ?? (object)DBNull.Value);
                    var id = await command.ExecuteScalarAsync();
                    return Convert.ToInt32(id);
                }
            }
        }

        public async Task UpdateTaskAsync(UserTask task)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand("UpdateTask", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Id", task.Id);
                    command.Parameters.AddWithValue("@Title", task.Title);
                    command.Parameters.AddWithValue("@Description", task.Description ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@IsCompleted", task.IsCompleted);
                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task DeleteTaskAsync(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand("DeleteTask", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Id", id);
                    await command.ExecuteNonQueryAsync();
                }
            }
        }
    }
}
