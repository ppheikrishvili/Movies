using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Movies.Domain.Interface;
using System.Reflection;
using Movies.Domain.Entity;

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

    public DbSet<ImdbUser> ImdbUsers { get; set; }

    public AppDBContext(DbContextOptions options) : base(options)
    {
        //ChangeTracker.LazyLoadingEnabled = false;
        ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        ApplyEntityMapsFromAssembly().ForEach(t => Activator.CreateInstance(t, modelBuilder));

        var _mockData = this.Database.GetService<ITestSeedsService>();
        if (_mockData != null)
        {
            var seedImdbUsers = _mockData.GetImdbUsers(10);
            modelBuilder.Entity<ImdbUser>().HasData(seedImdbUsers);
        }
    }
}