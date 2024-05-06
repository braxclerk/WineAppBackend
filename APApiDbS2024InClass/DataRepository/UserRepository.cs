using Npgsql;
using System;
using System.Threading.Tasks;
using APApiDbS2024InClass.Models;

namespace APApiDbS2024InClass.DataRepository
{
  public class UserRepository : BaseRepository
  {
    public async Task<User> RegisterUser(User user)
    {
      using (var dbConn = new NpgsqlConnection(ConnectionString))
      {
        var cmd = dbConn.CreateCommand();
        cmd.CommandText = "INSERT INTO users (first_name, last_name, email, password, dob, country, user_role) VALUES (@first_name, @last_name, @email, @password, @dob, @country, @user_role)";
        cmd.Parameters.AddWithValue("@first_name", user.FirstName);
        cmd.Parameters.AddWithValue("@last_name", user.LastName);
        cmd.Parameters.AddWithValue("@email", user.Email);
        cmd.Parameters.AddWithValue("@password", "password"); // For demonstration only
        cmd.Parameters.AddWithValue("@country", user.Country);
        cmd.Parameters.AddWithValue("@user_role", user.Role);
        dbConn.Open();
        user.Id = (int)await cmd.ExecuteScalarAsync();
        return user;
      }
    }

    public async Task<bool> UpdateUser(User user)
    {
      using (var dbConn = new NpgsqlConnection(ConnectionString))
      {
        var cmd = dbConn.CreateCommand();
        cmd.CommandText = "INSERT INTO users (first_name, last_name, email, password, dob, country, user_role) VALUES (@first_name, @last_name, @email, @password, @dob, @country, @user_role)";
        cmd.Parameters.AddWithValue("@first_name", user.FirstName);
        cmd.Parameters.AddWithValue("@last_name", user.LastName);
        cmd.Parameters.AddWithValue("@email", user.Email);
        cmd.Parameters.AddWithValue("@password", "password"); // For demonstration only
        cmd.Parameters.AddWithValue("@country", user.Country);
        cmd.Parameters.AddWithValue("@user_role", user.Role);

        dbConn.Open();
        var result = await cmd.ExecuteNonQueryAsync();
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
  }
}
