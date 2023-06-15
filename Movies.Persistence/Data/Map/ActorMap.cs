
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Movies.Domain.Entity;
using Movies.Domain.Interface;

namespace Movies.Persistence.Data.Map;

public class ActorMap : IBaseMapModel
{
    public ActorMap(ModelBuilder mBuilder)
    {
        EntityTypeBuilder<Actor> entityBuilder = mBuilder.Entity<Actor>();


    }
}