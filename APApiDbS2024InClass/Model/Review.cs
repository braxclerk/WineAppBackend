// Models/Review.cs

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Review
{
  [Key]
  public int ReviewId { get; set; }
  [Required]
  [ForeignKey("Wine")]
  public int WineId { get; set; }
  //public Wine Wine { get; set; }
  [Required]
  [ForeignKey("User")]
  public int UserId { get; set; }
  //public User User { get; set; }
  [Range(1, 5)]
  public int Rating { get; set; }  // Rating from 1 to 5
  [MaxLength(500)]
  public string Comment { get; set; }
  public DateTime ReviewDate { get; set; } = DateTime.Now;
}

