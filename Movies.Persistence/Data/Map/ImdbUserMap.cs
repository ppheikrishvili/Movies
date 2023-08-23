using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Movies.Domain.Entity;
using Movies.Domain.Interface;

namespace Movies.Persistence.Data.Map;

public class ImdbUserMap : IBaseMapModel
{
    public ImdbUserMap(ModelBuilder mBuilder)
    {
        EntityTypeBuilder<ImdbUser> entityBuilder = mBuilder.Entity<ImdbUser>();
        entityBuilder.HasKey(x => x.Name);
        //entityBuilder.Property(x => x.Name).HasMaxLength(32).IsRequired().IsUnicode();
        //entityBuilder.Property(x => x.Password).HasMaxLength(126).IsRequired().IsUnicode();
    }
}