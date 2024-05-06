using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Npgsql;
using APApiDbS2024InClass.Model;  // Ensure this is the correct namespace

namespace APApiDbS2024InClass.DataRepository
{
  public class ReviewRepository : IReviewRepository
  {
    private string _connectionString;

    public ReviewRepository(string connectionString)
    {
      _connectionString = connectionString;
    }

    public async Task AddReview(Review review)
    {
      using (var conn = new NpgsqlConnection(_connectionString))
      {
        var cmd = new NpgsqlCommand("INSERT INTO reviews (wine_id, user_id, comment, rating) VALUES (@wine_id, @user_id, @comment, @rating)", conn);
        cmd.Parameters.AddWithValue("@wine_id", review.WineId);
        cmd.Parameters.AddWithValue("@user_id", review.UserId);
        cmd.Parameters.AddWithValue("@comment", review.Comment);
        cmd.Parameters.AddWithValue("@rating", review.Rating);

        conn.Open();
        await cmd.ExecuteNonQueryAsync();
        conn.Close();
      }
    }

    public async Task<List<Review>> GetReviewsByWineId(int wineId)
    {
      var reviews = new List<Review>();
      using (var conn = new NpgsqlConnection(_connectionString))
      {
        var cmd = new NpgsqlCommand("SELECT * FROM reviews WHERE wine_id = @wine_id", conn);
        cmd.Parameters.AddWithValue("@wine_id", wineId);

        conn.Open();
        using (var reader = await cmd.ExecuteReaderAsync())
        {
          while (reader.Read())
          {
            reviews.Add(new Review
            {
              ReviewId = reader.GetInt32(reader.GetOrdinal("review_id")),
              UserId = reader.GetInt32(reader.GetOrdinal("user_id")),
              WineId = reader.GetInt32(reader.GetOrdinal("wine_id")),
              Comment = reader.GetString(reader.GetOrdinal("comment")),
              Rating = reader.GetInt32(reader.GetOrdinal("rating"))
            });
          }
        }
        conn.Close();
      }
      return reviews;
    }

    public async Task UpdateReview(Review review)
    {
      using (var conn = new NpgsqlConnection(_connectionString))
      {
        var cmd = new NpgsqlCommand("UPDATE reviews SET comment = @comment, rating = @rating WHERE review_id = @review_id", conn);
        cmd.Parameters.AddWithValue("@comment", review.Comment);
        cmd.Parameters.AddWithValue("@rating", review.Rating);
        cmd.Parameters.AddWithValue("@review_id", review.ReviewId);

        conn.Open();
        await cmd.ExecuteNonQueryAsync();
        conn.Close();
      }
    }

    public async Task<bool> DeleteReview(int reviewId)
    {
      using (var conn = new NpgsqlConnection(_connectionString))
      {
        var cmd = new NpgsqlCommand("DELETE FROM reviews WHERE review_id = @review_id", conn);
        cmd.Parameters.AddWithValue("@review_id", reviewId);

        conn.Open();
        var result = await cmd.ExecuteNonQueryAsync();
        conn.Close();

        return result > 0;
      }
    }
  }

  public interface IReviewRepository
  {
    Task AddReview(Review review);
    Task<List<Review>> GetReviewsByWineId(int wineId);
    Task UpdateReview(Review review);
    Task<bool> DeleteReview(int reviewId);
  }
}
