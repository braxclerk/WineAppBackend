using Npgsql;
using APApiDbS2024InClass.Model; // Ensure the namespace is correct
using System;

namespace APApiDbS2024InClass.DataRepository
{
  public class AdminRepository : UserRepository
  {
    // Method to insert a new user
    public bool InsertUser(User user)
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
        var result = cmd.ExecuteNonQuery();
        return result > 0;
      }
    }

    // Method to delete a user
    public bool DeleteUser(int id)
    {
      using (var dbConn = new NpgsqlConnection(ConnectionString))
      {
        var cmd = dbConn.CreateCommand();
        cmd.CommandText = "DELETE FROM users WHERE id = @id";
        cmd.Parameters.AddWithValue("@id", id);

        dbConn.Open();
        var result = cmd.ExecuteNonQuery();
        return result > 0;
      }
    }

    // Method to delete a review
    public bool DeleteReview(int reviewId)
    {
      using (var dbConn = new NpgsqlConnection(ConnectionString))
      {
        var cmd = dbConn.CreateCommand();
        cmd.CommandText = "DELETE FROM reviews WHERE id = @review_id";
        cmd.Parameters.AddWithValue("@review_id", reviewId);

        dbConn.Open();
        var result = cmd.ExecuteNonQuery();
        dbConn.Close();
        return result > 0;
      }
    }
  }
}
