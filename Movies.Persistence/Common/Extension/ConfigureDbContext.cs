using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using EFCore.AutomaticMigrations;
using Movies.Domain.Interface;
using Movies.Persistence.Seeds;

namespace Movies.Persistence.Common.Extension;

public static class ConfigureDbContext
{
    public static void AddDbContext(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        if (configuration.GetValue<bool>("IsInMemoryDatabase"))
        {
            serviceCollection.AddDbContext<AppDbContext>(options =>
                options.UseInMemoryDatabase("MovieIMDB"));
            serviceCollection.AddTransient<ITestSeedsService, TestSeedsService>();

            using ServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();
            var context = serviceProvider.GetRequiredService<AppDbContext>();
            context.Database.EnsureCreated();
        }
        else
        {
            serviceCollection.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("ConnectionString"),
                    b => b.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName)));

            using ServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();
            var context = serviceProvider.GetRequiredService<AppDbContext>();
            MigrateDatabaseToLatestVersion.Execute(context, new DbMigrationsOptions
            {
                ResetDatabaseSchema = true,
                AutomaticMigrationDataLossAllowed = true
            });
        }
    }
}