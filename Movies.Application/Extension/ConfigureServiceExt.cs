using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
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
        serviceCollection.AddValidatorsFromAssemblyContaining<ImdbUserValidation>();
    }


    public static void AddMedialRExt(this IServiceCollection serviceCollection) =>
        serviceCollection.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies()));

    public static void AddVersion(this IServiceCollection services)
    {
        services.AddApiVersioning(config =>
        {
            config.DefaultApiVersion = new ApiVersion(1, 0);
            config.AssumeDefaultVersionWhenUnspecified = true;
            config.ReportApiVersions = true;
        });
    }


    public static void AddVersionedApi(this IServiceCollection services)
    {
        services.AddVersionedApiExplorer(o =>
        {
            o.GroupNameFormat = "'v'VVV";
            o.SubstituteApiVersionInUrl = true;
        });
    }


    public static void AddSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = "Clean Architecture - Talent Demonstrate API",
                Description = "This API will be responsible for the demo.",
                Contact = new OpenApiContact
                {
                    Name = "Paata Pheikrishvili",
                    Email = "PaataPP@gmail.com",
                    Url = new Uri("http://blg.ge"),
                }
            });
        });
    }


    public static void AddCorsExt(this IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddPolicy("AllowAll",
                builder =>
                {
                    builder.AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
        });
    }


    public static void AddControllersExt(this IServiceCollection services, WebApplicationBuilder builder)
    {
        services.AddControllers(options =>
        {
            //options.CacheProfiles.Add("Cache2Mins",
            //    new CacheProfile()
            //    {
            //        Duration = 120,
            //        Location = ResponseCacheLocation.Any
            //    });
            var cacheProfiles = builder.Configuration
                .GetSection("CacheProfiles")
                .GetChildren();
            foreach (var cacheProfile in cacheProfiles)
            {
                options.CacheProfiles
                    .Add(cacheProfile.Key,
                        cacheProfile.Get<CacheProfile>() ?? throw new InvalidOperationException());
            }
        });
    }


    //public static WebApplication ApplyMigrations(this WebApplication app)
    //{
    //    using var scope = app.Services.CreateScope();
    //    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    //    db.Database.Migrate();
    //    return app;
    //}
}