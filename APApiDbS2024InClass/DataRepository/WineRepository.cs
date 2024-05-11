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

        public List<Wine> GetWines()
        {
            var wines = new List<Wine>();
            using (var dbConn = new NpgsqlConnection(ConnectionString))
            {
                dbConn.Open(); // Ensure the connection is explicitly opened.
                var cmd = dbConn.CreateCommand();
                cmd.CommandText = "SELECT * FROM wines";

                var data = cmd.ExecuteReader(); // Adjusted to directly use ExecuteReader
                if (data.HasRows) // Check if data has rows
                {
                    while (data.Read())
                    {
                        Console.WriteLine(data);
                        wines.Add(new Wine
                        {
                            Id = Convert.ToInt32(data["id"]),
                            Name = data["name"].ToString(),
                            Color = data["color"].ToString(),
                            Taste = Convert.ToInt32(data["taste"]),
                            Country = data["country"].ToString(),
                            Description = data["description"].ToString(),
                            Age = Convert.ToInt32(data["age"]),
                            UserId = Convert.ToInt32(data["user_id"])

                        });
                    }
                }
                return wines;
            } }

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
                            Age = Convert.ToInt32(data["age"]),
                            UserId = Convert.ToInt32(data["user_id"])
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
                    cmd.CommandText = "INSERT INTO wines (name, color, taste, country, description, age, user_id) VALUES (@name, @color, @taste, @country, @description, @age, @user_id)";
                    cmd.Parameters.AddWithValue("@name", NpgsqlDbType.Text, wine.Name);
                    cmd.Parameters.AddWithValue("@color", NpgsqlDbType.Text, wine.Color);
                    cmd.Parameters.AddWithValue("@taste", NpgsqlDbType.Integer, wine.Taste);
                    cmd.Parameters.AddWithValue("@country", NpgsqlDbType.Text, wine.Country);
                    cmd.Parameters.AddWithValue("@description", NpgsqlDbType.Text, wine.Description);
                    cmd.Parameters.AddWithValue("@age", NpgsqlDbType.Integer, wine.Age);
                    cmd.Parameters.AddWithValue("@user_id", NpgsqlDbType.Integer, wine.UserId);  // Assuming wine.UserId is the property holding the user ID

                    return InsertData(dbConn, cmd);
                }
            }


            public bool UpdateWine(int id, Wine wine)
            {
                using (var dbConn = new NpgsqlConnection(ConnectionString))
                {
                    var cmd = dbConn.CreateCommand();
                    cmd.CommandText = "UPDATE wines SET name = @name, color = @color, taste = @taste, country = @country, description = @description, age = @age, user_id = @user_id WHERE id = @id";
                    cmd.Parameters.AddWithValue("@id", NpgsqlDbType.Integer, id);
                    cmd.Parameters.AddWithValue("@name", NpgsqlDbType.Text, wine.Name);
                    cmd.Parameters.AddWithValue("@color", NpgsqlDbType.Text, wine.Color);
                    cmd.Parameters.AddWithValue("@taste", NpgsqlDbType.Integer, wine.Taste);
                    cmd.Parameters.AddWithValue("@country", NpgsqlDbType.Text, wine.Country);
                    cmd.Parameters.AddWithValue("@description", NpgsqlDbType.Text, wine.Description);
                    cmd.Parameters.AddWithValue("@age", NpgsqlDbType.Integer, wine.Age);
                    cmd.Parameters.AddWithValue("@user_id", NpgsqlDbType.Integer, wine.UserId);  // Same here for UserId

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

