using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BeToff.Entities;

public class Comment : BeToffEntity
{
    [Required]
    [MaxLength(800)]
    public string Content {get; set;}
    [Required]
    public DateOnly DateCreation {get; set;}

    [ForeignKey("Author")]
    
    
    public Guid IdAuthor { get; set; }

    //[Column("Author")]
    [DeleteBehavior(DeleteBehavior.NoAction)]
    public User Author { get; set; }

    [ForeignKey("Photo")]
    
    public Guid IdPhoto { get; set; }

    [DeleteBehavior(DeleteBehavior.Cascade)]
    public Photo Photo {get; set;}
    
    
}