using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Movies.Domain.Interface;

namespace Movies.Domain.Entity;

public class Entity : IEntity
{
    [Key, MaxLength(32)]
    [JsonPropertyName("id")]
    //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public required string id { get; set; }
}