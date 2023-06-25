using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using Movies.Application.Validators;

namespace Movies.Application.Extension;

public static class ConfigureServiceExt
{
    public static void AddFluent(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddFluentValidationAutoValidation();
        serviceCollection.AddFluentValidationClientsideAdapters();
        serviceCollection.AddValidatorsFromAssemblyContaining<ActorValidation>();
        serviceCollection.AddValidatorsFromAssemblyContaining<MovieValidation>();
        serviceCollection.AddValidatorsFromAssemblyContaining<ActorAwardValidation>();
    }


    public static void AddMedialR(this IServiceCollection serviceCollection) =>
        serviceCollection.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies()));


    //public static WebApplication ApplyMigrations(this WebApplication app)
    //{
    //    using var scope = app.Services.CreateScope();
    //    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    //    db.Database.Migrate();
    //    return app;
    //}
}