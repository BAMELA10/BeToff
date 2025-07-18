using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace BeToff.Entities;

public class Photo : BeToffEntity
{

    [Required]
    [ForeignKey(nameof(Author))]
    public Guid AuthorId { get; set; }

    //[Column("Author")]
    [DeleteBehavior(DeleteBehavior.Cascade)]
    public User Author { get; set; }

    [Required]
    public DateOnly DateCreation {get; set;}

    [Required]
    public string Image {get; set;}

    [Required]
    public string Title { get; set; }

    //public ICollection<Comment> Comments {get; set;}
}