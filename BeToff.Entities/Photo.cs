using System.ComponentModel.DataAnnotations;
namespace BeToff.Entities;

public class Photo : BeToffEntity
{
    [Required]
    public User CreatedBy {get; set;}
    [Required]
    public DateOnly DateCreation {get; set;}
    [Required]
    public string Image {get; set;}
    
    public ICollection<Comment> Comments {get; set;}
}