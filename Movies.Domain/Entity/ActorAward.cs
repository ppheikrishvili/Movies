
using Movies.Domain.Interface;
using System.ComponentModel.DataAnnotations;

namespace Movies.Domain.Entity;

public class ActorAward : IEntity
{
    [MaxLength(32)]
    public required string ActorId { get; set; }
    [MaxLength(32)]
    public required string MovieId { get; set; }
    [MaxLength(32)]
    public required string AwardName { get; set; }
    public Actor? Actor { get; set; }
    public Movie? Movie { get; set; }
}