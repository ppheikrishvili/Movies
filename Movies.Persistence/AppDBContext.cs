using Microsoft.EntityFrameworkCore;
using Movies.Domain.Interface;
using System.Reflection;

namespace Movies.Persistence;

public sealed class AppDBContext : DbContext
{
    internal static List<Type> ApplyEntityMapsFromAssembly() => Assembly.GetCallingAssembly().GetTypes()
        .Where(w => typeof(IBaseMapModel).IsAssignableFrom(w) &&
                    w.IsClass && w is {BaseType: not null, IsInterface: false, IsAbstract: false}).ToList();

    public AppDBContext()
    {
        ChangeTracker.LazyLoadingEnabled = false;
        ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
    }

    public AppDBContext(DbContextOptions options) : base(options)
    {
        ChangeTracker.LazyLoadingEnabled = false;
        ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder) =>
        ApplyEntityMapsFromAssembly().ForEach(t => Activator.CreateInstance(t, modelBuilder));

}
