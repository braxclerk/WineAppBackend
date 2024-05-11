// Models/Wine.cs

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Wine
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string Name { get; set; }

    //[Required]
    public string Color { get; set; }

    [Range(1, 10)]
    public int Taste { get; set; } // Rating from 1 to 10

    //[Required]
    public string Country { get; set; }

    //[Required]
    public string Description { get; set; }

    //[Required]
    public int Age { get; set; } // Age in years

    [Required]
    [ForeignKey(nameof(User))]
    public int UserId { get; set; }

}
