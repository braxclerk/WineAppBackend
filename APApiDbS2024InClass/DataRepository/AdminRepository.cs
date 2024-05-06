using Npgsql;
using APApiDbS2024InClass.Model; // Ensure the namespace is correct
using System.Threading.Tasks;

namespace APApiDbS2024InClass.DataRepository
{
  public class AdminRepository : UserRepository
  {
    // Asynchronously insert a new user
    public async Task<bool> InsertUserAsync(User user)
    {
      using (var dbConn = new NpgsqlConnection(ConnectionString))
      {
        var cmd = dbConn.CreateCommand();
        cmd.CommandText = "INSERT INTO users (first_name, last_name, email, password, dob, country, user_role) VALUES (@first_name, @last_name, @email, @password, @dob, @country, @user_role)";
        cmd.Parameters.AddWithValue("@first_name", user.FirstName);
        cmd.Parameters.AddWithValue("@last_name", user.LastName);
        cmd.Parameters.AddWithValue("@email", user.Email);
        cmd.Parameters.AddWithValue("@password", user.Password); // Ensure this is hashed if not demo
        cmd.Parameters.AddWithValue("@country", user.Country);
        cmd.Parameters.AddWithValue("@user_role", user.Role);

        dbConn.Open();
        var result = await cmd.ExecuteNonQueryAsync();
        return result > 0;
      }
    }

    // Asynchronously delete a user
    public async Task<bool> DeleteUserAsync(int userId)
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

    // Asynchronously delete a review
    public async Task<bool> DeleteReviewAsync(int reviewId)
    {
      using (var dbConn = new NpgsqlConnection(ConnectionString))
      {
        var cmd = dbConn.CreateCommand();
        cmd.CommandText = "DELETE FROM reviews WHERE id = @review_id";
        cmd.Parameters.AddWithValue("@review_id", reviewId);

        dbConn.Open();
        var result = await cmd.ExecuteNonQueryAsync();
        dbConn.Close();
        return result > 0;
      }
    }
    // Asynchronously retrieve all users
    public async Task<List<User>> GetAllUsersAsync()
    {
      var users = new List<User>();
      using (var dbConn = new NpgsqlConnection(ConnectionString))
      {
        var cmd = dbConn.CreateCommand();
        cmd.CommandText = "SELECT * FROM users";

        dbConn.Open();
        using (var reader = await cmd.ExecuteReaderAsync())
        {
          while (reader.Read())
          {
            users.Add(new User
            {
              Id = reader.GetInt32(reader.GetOrdinal("id")),
              FirstName = reader.GetString(reader.GetOrdinal("first_name")),
              LastName = reader.GetString(reader.GetOrdinal("last_name")),
              Email = reader.GetString(reader.GetOrdinal("email")),
              Password = reader.GetString(reader.GetOrdinal("password")), // Note: Passwords should not be retrieved like this in a real app
              Country = reader.GetString(reader.GetOrdinal("country")),
              Role = reader.GetString(reader.GetOrdinal("user_role"))
            });
          }
        }
      }
      return users;
    }
  }
}
