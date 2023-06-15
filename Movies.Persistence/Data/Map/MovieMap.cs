using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Movies.Domain.Entity;
using Movies.Domain.Interface;

namespace Movies.Persistence.Data.Map;

public class MovieMap : IBaseMapModel
{
    public MovieMap(ModelBuilder mBuilder)
    {
        EntityTypeBuilder<Movie> entityBuilder = mBuilder.Entity<Movie>();

        //entityBuilder.HasOne(s => s.Actor).WithMany(i =>
        //    i.Movies).HasForeignKey(h => h.ActorId).HasPrincipalKey(p => p.id);
    }
}