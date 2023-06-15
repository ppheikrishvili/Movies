using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Movies.Domain.Entity;
using Movies.Domain.Interface;

namespace Movies.Persistence.Data.Map;

public class ActorAwardMap : IBaseMapModel
{
    public ActorAwardMap(ModelBuilder mBuilder)
    {
        EntityTypeBuilder<ActorAward> entityBuilder = mBuilder.Entity<ActorAward>();
        entityBuilder.HasKey(x => new { x.ActorId, x.MovieId, x.AwardName });
        entityBuilder.HasOne(s => s.Actor).WithMany(i =>
            i.ActorAwards).HasForeignKey(h => h.ActorId).HasPrincipalKey(p => p.id);
        entityBuilder.HasOne(s => s.Movie).WithMany(i =>
            i.ActorAwards).HasForeignKey(h => h.MovieId).HasPrincipalKey(p => p.id);
    }
}