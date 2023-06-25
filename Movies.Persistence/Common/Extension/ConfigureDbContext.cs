using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using EFCore.AutomaticMigrations;

namespace Movies.Persistence.Common.Extension;

public static class ConfigureDbContext
{
    public static void AddDbContext(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        serviceCollection.AddDbContext<AppDBContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("ConnectionString"),
                b => b.MigrationsAssembly(typeof(AppDBContext).Assembly.FullName)));

        using ServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();
        var context = serviceProvider.GetRequiredService<AppDBContext>();
        MigrateDatabaseToLatestVersion.Execute(context, new DbMigrationsOptions
        {
            ResetDatabaseSchema = true,
            AutomaticMigrationDataLossAllowed = true
        });
    }
}