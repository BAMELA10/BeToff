using System.ComponentModel.DataAnnotations;

namespace BeToff.Entities;

public class Comment : BeToffEntity
{
    [Required]
    [MaxLength(800)]
    public string Content {get; set;}
    [Required]
    public DateOnly DateCreation {get; set;}
    [Required]
    public User CreatedBy {get; set;}
    [Required]
    public Photo Photo {get; set;}


}