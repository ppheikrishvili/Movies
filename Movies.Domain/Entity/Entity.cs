using System.ComponentModel.DataAnnotations;
using Movies.Domain.Interface;

namespace Movies.Domain.Entity;

public class Entity : IEntity
{
    [Key, MaxLength(32)]
    public required string id { get; set; }
}