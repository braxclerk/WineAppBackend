using Npgsql;
using System;
using System.Threading.Tasks;
using APApiDbS2024InClass.Model;
using System.Security.Cryptography;
using System.Text;

namespace APApiDbS2024InClass.DataRepository
{
  public class UserRepository : BaseRepository
  {
        public async Task<List<User>> GetAllUsers()
        {
            List<User> users = new List<User>();
            using (var dbConn = new NpgsqlConnection(ConnectionString))
            {
                await dbConn.OpenAsync();
                var cmd = new NpgsqlCommand("SELECT * FROM users", dbConn);
                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (reader.Read())
                    {
                        users.Add(new User
                        {
                            Id = Convert.ToInt32(reader["id"]),
                            FirstName = reader["first_name"].ToString(),
                            LastName = reader["last_name"].ToString(),
                            Email = reader["email"].ToString(),
                            Username = reader["username"].ToString(),  // Retrieve username
                            Country = reader["country"].ToString(),
                            Role = reader["user_role"].ToString()
                        });
                    }
                }
            }
            return users;
        }

        public async Task<User> RegisterUser(User user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user), "Provided user is null.");

            using (var dbConn = new NpgsqlConnection(ConnectionString))
            {
                var cmd = dbConn.CreateCommand();
                cmd.CommandText = @"
            INSERT INTO users (first_name, last_name, email, username, password, country, user_role) 
            VALUES (@first_name, @last_name, @email, @username, @password, @country, @user_role)
            RETURNING id;";

                cmd.Parameters.AddWithValue("@first_name", user.FirstName);
                cmd.Parameters.AddWithValue("@last_name", user.LastName);
                cmd.Parameters.AddWithValue("@email", user.Email);
                cmd.Parameters.AddWithValue("@username", user.Username); // Include username in the INSERT
                cmd.Parameters.AddWithValue("@password", user.Password);
                cmd.Parameters.AddWithValue("@country", user.Country);
                cmd.Parameters.AddWithValue("@user_role", user.Role);

                dbConn.Open();
                var result = await cmd.ExecuteScalarAsync();
                if (result == null)
                    throw new InvalidOperationException("Failed to register the user in the database.");

                user.Id = Convert.ToInt32(result);
                return user;
            }
        }




        public async Task<bool> UpdateUser(User user)
    {
      using (var dbConn = new NpgsqlConnection(ConnectionString))
      {
        var cmd = dbConn.CreateCommand();
        cmd.CommandText = @"
            UPDATE users 
            SET first_name = @first_name, 
                last_name = @last_name, 
                email = @email, 
                password = @password, 
                country = @country, 
                user_role = @user_role
            WHERE id = @id";

        cmd.Parameters.AddWithValue("@first_name", user.FirstName);
        cmd.Parameters.AddWithValue("@last_name", user.LastName);
        cmd.Parameters.AddWithValue("@email", user.Email);
        cmd.Parameters.AddWithValue("@password", user.Password); // Make sure this password is hashed
        cmd.Parameters.AddWithValue("@country", user.Country);
        cmd.Parameters.AddWithValue("@user_role", user.Role);
        cmd.Parameters.AddWithValue("@id", user.Id);

        dbConn.Open();
        var result = await cmd.ExecuteNonQueryAsync();
        return result > 0; // returns true if at least one record was updated
      }
    }


        public async Task<bool> ValidateUser(string username, string password)
        {
            using (var dbConn = new NpgsqlConnection(ConnectionString))
            {
                await dbConn.OpenAsync();
                var cmd = new NpgsqlCommand("SELECT COUNT(*) FROM users WHERE username = @username AND password = @password", dbConn);
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@password", password);  // Consider using hashed passwords

                var result = (long)await cmd.ExecuteScalarAsync();
                return result > 0;
            }
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

    public async Task<List<User>> GetUsersByRole(string roleId)
    {
      List<User> result = new List<User>();
      using (var dbConn = new NpgsqlConnection(ConnectionString))
      {
        await dbConn.OpenAsync();
        var cmd = new NpgsqlCommand("SELECT * FROM users WHERE user_role = @user_role", dbConn);
        cmd.Parameters.AddWithValue("@user_role", roleId);

        using (var reader = await cmd.ExecuteReaderAsync())
        {
          while (reader.Read())
          {
            var user = new User
            {
              Id = Convert.ToInt32(reader["id"]),
              FirstName = reader["first_name"].ToString(),
              LastName = reader["last_name"].ToString(),
              Email = reader["email"].ToString(), 
              Country = reader["country"].ToString(),
              Role = reader["user_role"].ToString()
            };
            result.Add(user);
          }
        }
      }
      return result;
    }


  }

}
