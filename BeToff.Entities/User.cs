using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BeToff.Entities;

public class User : BeToffEntity
{

    public string? FirstName {get; set;}

    [Required]
    [MaxLength(100)]
    public string? LastName {get; set;}
    public DateOnly DateJoined {get; set;}
    public string FullName => string.Format("{0} {1}", LastName, FirstName);

    [Required]
    [MaxLength(100)]
    public string? Password {get; set;}

    [MaxLength(100)]
    public string? PhoneNumber {get; set;}

    [Required]
    [MaxLength(100)]
    [EmailAddress]
    public string? Email {get; set;}

    [Required]
    [DefaultValue(false)]
    public bool? IsActive {get; set;}

    public ICollection<Photo> Photos { get;} = new List<Photo>();

    public IEnumerable<Familly> Famillies { get; set; } = new List<Familly>();

    public IEnumerable<Familly> FamilliesHeadof { get; set; } = new List<Familly>();

    public IEnumerable<Familly> FamilliesCreator { get; set; } = new List<Familly>();

    public IEnumerable<Registration> Registrations { get; set; } = new List<Registration>();

    //public ICollection<Comment>? Comments {get; set;}
}