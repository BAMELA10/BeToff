using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BeToff.Entities;

public class Familly : BeToffEntity
{

    [Column("NameOfFamilly")]
    [Required]
    [MaxLength(100)]
    public string Name {get; set;}

    [Required]
    public DateOnly DateCreation {get; set;}

    [ForeignKey("CreatedBy")]
    public Guid? IdCreator {get; set;}

    //[Column("Founder")]
    [DeleteBehavior(DeleteBehavior.NoAction)]
    public User CreatedBy { get; set; }

    [ForeignKey("Headof")]
    public Guid IdHead { get; set; }

    [Column("HeadOfFamilly")]
    [DeleteBehavior(DeleteBehavior.NoAction)]
    public User Headof {get; set;}

    public IEnumerable<User> Members { get; set; } = new List<User>();

    public IEnumerable<Registration> Registrations { get; set; } = new List<Registration>();

    public IEnumerable<Invitation> Invitations { get; set; } = new List<Invitation>();
}
