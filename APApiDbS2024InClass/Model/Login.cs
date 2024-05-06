using System;
using System.ComponentModel.DataAnnotations;

namespace APApiDbS2024InClass.Model
{
  public class Login
  {
    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    public Login()
    {
    }

    public Login(string email, string password)
    {
      Email = email;
      Password = password;
    }
  }
}
