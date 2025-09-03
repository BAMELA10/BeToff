using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BeToff.Entities;

public class Comment : ComEntity
{
    
    [Required]
    [MaxLength(800)]
    public string Content {get; set;}

    [Required]
    public DateOnly PubliedAt {get; set;}
    
    public string Author { get; set; }

    public string PhotoFamily { get; set; }
}