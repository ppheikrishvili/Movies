using System.ComponentModel.DataAnnotations;

namespace Movies.Domain.Entity;

public class Actor : Entity
{
    [MaxLength(150)]
    public string? name { get; set; }

    public ICollection<Movie>? Movies { get; set; }
    public ICollection<ActorAward>? ActorAwards { get; set; }
}