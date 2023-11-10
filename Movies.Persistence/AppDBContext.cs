using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Movies.Domain.Interface;
using System.Reflection;
using Movies.Domain.Entity;

namespace Movies.Persistence;

public sealed class AppDbContext : DbContext
{
    internal static List<Type> ApplyEntityMapsFromAssembly() => Assembly.GetCallingAssembly().GetTypes()
        .Where(w => typeof(IBaseMapModel).IsAssignableFrom(w) &&
                    w.IsClass && w is {BaseType: not null, IsInterface: false, IsAbstract: false}).ToList();

    public AppDbContext()
    {
        if (!Database.IsInMemory()) ChangeTracker.LazyLoadingEnabled = false;
        ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
    }

    public AppDbContext(DbContextOptions options) : base(options)
    {
        if (!Database.IsInMemory()) ChangeTracker.LazyLoadingEnabled = false;
        ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        ApplyEntityMapsFromAssembly().ForEach(t => Activator.CreateInstance(t, modelBuilder));

        var seedData = Database.GetService<ITestSeedsService>();
        var seedImdbUsers = seedData.GetImdbUsers(10);
       // var seedImdbActors = seedData.GetImdbActors(10);
        modelBuilder.Entity<ImdbUser>().HasData(seedImdbUsers);
        //modelBuilder.Entity<Actor>().HasData(seedImdbActors);
    }
}