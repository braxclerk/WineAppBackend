using System;
using System.Threading.Tasks;
using APApiDbS2024InClass.Models;
using Npgsql;

namespace APApiDbS2024InClass.DataRepository
{
    public class UserRepository : BaseRepository
    {
        public async Task<User> InsertGuestUser(User user)
        {
            using (var dbConn = new NpgsqlConnection(ConnectionString))
            {
                var cmd = dbConn.CreateCommand();
                cmd.CommandText =
                    "INSERT INTO users (first_name, last_name, email, password, country, user_role) VALUES (@first_name, @last_name, @email, @password, @dob, @country, @user_role)";
                cmd.Parameters.AddWithValue("@first_name", user.FirstName);
                cmd.Parameters.AddWithValue("@last_name", user.LastName);
                cmd.Parameters.AddWithValue("@email", user.Email);
                cmd.Parameters.AddWithValue("@password", "password"); // For demonstration only
                cmd.Parameters.AddWithValue("@country", user.Country);
                cmd.Parameters.AddWithValue("@user_role", user.Role);
                dbConn.Open();
                user.Id = (int) await cmd.ExecuteScalarAsync();
                return user;
            }
        }

        public async Task<bool> UpdateUser(User user)
        {
            using (var dbConn = new NpgsqlConnection(ConnectionString))
            {
                var cmd = dbConn.CreateCommand();
                cmd.CommandText =
                    "INSERT INTO users (first_name, last_name, email, password, country, user_role) VALUES (@first_name, @last_name, @email, @password, @country, @user_role)";
                cmd.Parameters.AddWithValue("@first_name", user.FirstName);
                cmd.Parameters.AddWithValue("@last_name", user.LastName);
                cmd.Parameters.AddWithValue("@email", user.Email);
                cmd.Parameters.AddWithValue("@password", user.Password); // For demonstration only
                cmd.Parameters.AddWithValue("@country", user.Country);
                cmd.Parameters.AddWithValue("@user_role", user.Role);

                dbConn.Open();
                var result = await cmd.ExecuteNonQueryAsync();
                // Are we sure a success from adding to db is a integer?
                return result > 0;
            }
        }

        public async Task<bool> InsertUser(User user)
        {
            using (var dbConn = new NpgsqlConnection(ConnectionString))
            {
                var cmd = dbConn.CreateCommand();
                cmd.CommandText =
                    "INSERT INTO users (first_name, last_name, email, password, country, user_role) VALUES (@first_name, @last_name, @email, @password, @country, @user_role)";
                cmd.Parameters.AddWithValue("@first_name", user.FirstName);
                cmd.Parameters.AddWithValue("@last_name", user.LastName);
                cmd.Parameters.AddWithValue("@email", user.Email);
                cmd.Parameters.AddWithValue("@password", user.Password); // For demonstration only
                cmd.Parameters.AddWithValue("@country", user.Country);
                cmd.Parameters.AddWithValue("@user_role", user.Role);

                dbConn.Open();
                var result = await cmd.ExecuteNonQueryAsync();
                // Are we sure a success from adding to db is a integer?
                return result > 0;
            }
        }

        public async Task<List<User>> GetUsersByRole(string roleId)
        {
            List<User> result = new List<User>();
            using (var dbConn = new NpgsqlConnection(ConnectionString))
            {
                var cmd = dbConn.CreateCommand();
                cmd.CommandText = "FROM users SELECT * WHERE user_role = @user_role";
                cmd.Parameters.AddWithValue("@user_role", roleId);

                var data = GetData(dbConn, cmd);
                if (data != null && data.Read())
                {
                    var titty = new User(
                        Convert.ToInt32(data["id"]),
                        data["first_name"].ToString(),
                        data["last_name"].ToString(),
                        data["email"].ToString(),
                        data["password"].ToString(),
                        data["country"].ToString(),
                        data["user_role"].ToString()
                    );
                    result.Add(titty);
                }
            }
            return result;
        }

        public async Task<bool> DeleteUser(int userId)
        {
            using (var dbConn = new NpgsqlConnection(ConnectionString))
            {
                var cmd = dbConn.CreateCommand();
                cmd.CommandText = "DELETE FROM users WHERE id = @id";
                cmd.Parameters.AddWithValue("@id", userId);

                dbConn.Open();
                var result = await cmd.ExecuteNonQueryAsync();
                return result > 0;
            }
        }
    }
}
