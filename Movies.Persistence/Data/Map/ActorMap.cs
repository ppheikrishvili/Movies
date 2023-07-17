using Microsoft.EntityFrameworkCore;
using Movies.Domain.Entity;
using Movies.Domain.Interface;

namespace Movies.Persistence.Data.Map;

public class ActorMap : IBaseMapModel
{
    public ActorMap(ModelBuilder mBuilder)
    {
        mBuilder.Entity<Actor>();
    }
}