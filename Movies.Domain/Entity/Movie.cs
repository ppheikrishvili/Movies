using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Movies.Domain.Entity;

public class Movie : Entity
{
    [MaxLength(150)]
    [JsonPropertyName("title")]
    public string title { get; set; }

    [MaxLength(250)]
    [JsonPropertyName("description")]
    public string? description { get; set; }

    [MaxLength(32)]
    //public string? ActorId { get; set; }
    public ICollection<Actor>? Actors { get; set; }

    public ICollection<ActorAward>? ActorAwards { get; set; }
}