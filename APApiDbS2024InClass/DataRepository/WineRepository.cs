using Npgsql;
using NpgsqlTypes;
using System.Collections.Generic;

namespace APApiDbS2024InClass.DataRepository
{
  public class WineRepository : BaseRepository

  {
    private readonly string _connectionString;

    public WineRepository(string connectionString)
    {
      _connectionString = connectionString;
    }

    // Get all wines
    public List<Wine> GetWines()
    {
      var wines = new List<Wine>();
      using (var dbConn = new NpgsqlConnection(ConnectionString))
      {
        var cmd = dbConn.CreateCommand();
        cmd.CommandText = "SELECT * FROM wines";

        var data = GetData(dbConn, cmd);
        if (data != null)
        {
          while (data.Read())
          {
            wines.Add(new Wine
            {
              Id = Convert.ToInt32(data["id"]),
              Name = data["name"].ToString(), // Added name
              Color = data["color"].ToString(),
              Taste = Convert.ToInt32(data["taste"]),
              Country = data["country"].ToString(),
              Description = data["description"].ToString(),
              Age = Convert.ToInt32(data["age"])
            });
          }
        }
      }
      return wines;
    }

    // Get a single wine by ID
    public Wine GetWineById(int id)
    {
      using (var dbConn = new NpgsqlConnection(ConnectionString))
      {
        var cmd = dbConn.CreateCommand();
        cmd.CommandText = "SELECT * FROM wines WHERE id = @id";
        cmd.Parameters.AddWithValue("@id", NpgsqlDbType.Integer, id);

        var data = GetData(dbConn, cmd);
        if (data != null && data.Read())
        {
          return new Wine
          {
            Id = Convert.ToInt32(data["id"]),
            Name = data["name"].ToString(), // Added name
            Color = data["color"].ToString(),
            Taste = Convert.ToInt32(data["taste"]),
            Country = data["country"].ToString(),
            Description = data["description"].ToString(),
            Age = Convert.ToInt32(data["age"])
          };
        }
      }
      return null;
    }

    // Insert a new wine
    public bool InsertWine(Wine wine)
    {
      using (var dbConn = new NpgsqlConnection(ConnectionString))
      {
        var cmd = dbConn.CreateCommand();
        cmd.CommandText = "INSERT INTO wines (name, color, taste, country, description, age) VALUES (@name, @color, @taste, @country, @description, @age)";
        cmd.Parameters.AddWithValue("@name", NpgsqlDbType.Text, wine.Name); // Added name
        cmd.Parameters.AddWithValue("@color", NpgsqlDbType.Text, wine.Color);
        cmd.Parameters.AddWithValue("@taste", NpgsqlDbType.Integer, wine.Taste);
        cmd.Parameters.AddWithValue("@country", NpgsqlDbType.Text, wine.Country);
        cmd.Parameters.AddWithValue("@description", NpgsqlDbType.Text, wine.Description);
        cmd.Parameters.AddWithValue("@age", NpgsqlDbType.Integer, wine.Age);

        return InsertData(dbConn, cmd);
      }
    }

    // Update a wine
    public bool UpdateWine(int id, Wine wine)
    {
      using (var dbConn = new NpgsqlConnection(ConnectionString))
      {
        var cmd = dbConn.CreateCommand();
        cmd.CommandText = "UPDATE wines SET name = @name, color = @color, taste = @taste, country = @country, description = @description, age = @age WHERE id = @id";
        cmd.Parameters.AddWithValue("@id", NpgsqlDbType.Integer, id);
        cmd.Parameters.AddWithValue("@name", NpgsqlDbType.Text, wine.Name); // Added name
        cmd.Parameters.AddWithValue("@color", NpgsqlDbType.Text, wine.Color);
        cmd.Parameters.AddWithValue("@taste", NpgsqlDbType.Integer, wine.Taste);
        cmd.Parameters.AddWithValue("@country", NpgsqlDbType.Text, wine.Country);
        cmd.Parameters.AddWithValue("@description", NpgsqlDbType.Text, wine.Description);
        cmd.Parameters.AddWithValue("@age", NpgsqlDbType.Integer, wine.Age);

        return UpdateData(dbConn, cmd);
      }
    }

    public async Task<bool> DeleteWine(int wineId)
    {
      using (var conn = new NpgsqlConnection(_connectionString))
      {
        var cmd = new NpgsqlCommand("DELETE FROM wines WHERE id = @Id", conn);
        cmd.Parameters.AddWithValue("@Id", wineId);
        conn.Open();
        var result = await cmd.ExecuteNonQueryAsync();
        return result > 0;
      }
    }
  }
}
