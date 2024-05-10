using System;
using System.ComponentModel.DataAnnotations;

public class User
{
  public User(int id)
  {
    Id = id;
  }

  public User(int id, string firstName, string lastName, string email, string password, string country, string role = "User")
  {
    Id = id;
    FirstName = firstName;
    LastName = lastName;
    Email = email;
    Password = password; // This should be hashed before being set ideally WRITE IN PAPER
    Country = country;
    Role = role;
  }

  public User() { }

  [Key]
  public int Id { get; set; }

  [Required]
  [MaxLength(100)]
  public string FirstName { get; set; }

  [Required]
  [MaxLength(100)]
  public string LastName { get; set; }

  [Required]
  [EmailAddress]
  [MaxLength(255)]
  public string Email { get; set; }

  [Required]
  public string Password { get; set; }

  [Required]
  public string Country { get; set; }

  [Required]
  public string Role { get; set; }

   [Required]
   public string Username { get; set; }
}
