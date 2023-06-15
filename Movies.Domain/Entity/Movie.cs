using System.ComponentModel.DataAnnotations;

namespace Movies.Domain.Entity;

public class Movie : Entity
{
    [MaxLength(150)]
    public required string title { get; set; }
    [MaxLength(250)]
    public string? description { get; set; }
    [MaxLength(32)]
    //public string? ActorId { get; set; }
    public ICollection<Actor>? Actors { get; set; }
    public ICollection<ActorAward>? ActorAwards { get; set; }
}
