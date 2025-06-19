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
    [Required]
    public User CreatedBy {get; set;}

    [Column("HeadOfFamilly")]
    public User Headof {get; set;}
}
