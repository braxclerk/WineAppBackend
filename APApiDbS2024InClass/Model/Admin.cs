using System;
using System.ComponentModel.DataAnnotations;

namespace APApiDbS2024InClass.Models
{
  public class Admin : User
  {
    [Required]
    public string Role { set; get; }

    public Admin(int id, string role) : base(id)
    {
      Role = role;  // Use this setup if Role isn't set by default in the User base class
    }
  }
}
