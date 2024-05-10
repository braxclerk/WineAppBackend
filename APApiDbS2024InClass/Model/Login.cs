using System.Text.Json.Serialization;

namespace APApiDbS2024InClass.Model;

public class Login
{
    [JsonPropertyName("username")]
    public string Username { get; set; }  // Changed from 'Username' to 'Email' and specified 'string' as the type

    [JsonPropertyName("password")]
    public string Password { get; set; }  // Specified 'string' as the type for the password
}
