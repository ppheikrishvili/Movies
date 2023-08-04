using System.Text.Json.Serialization;
using MediatR;
using Movies.Application.Behaviors;
using Movies.Application.Extension;
using Movies.Domain.Interface;
using Movies.Infrastructure.Middleware;
using Movies.Persistence.Common.Extension;
using Movies.Persistence.Data.Repository;


//https://learn.microsoft.com/en-us/dotnet/architecture/microservices/microservice-ddd-cqrs-patterns/microservice-application-layer-implementation-web-api

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersExt(builder);
builder.Services.AddMedialRExt();
builder.Services.AddFluent();

builder.Services.AddVersion();
builder.Services.AddMvcCore().AddApiExplorer();
builder.Services.AddVersionedApi();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwagger();

builder.Services.AddTransient<GlobalExceptionHandler>();

builder.Services.AddCorsExt();

builder.Services.AddHttpClientExt(builder.Configuration);

builder.Services.AddResponseCaching();

builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

builder.Services.AddDbContext(builder.Configuration);

builder.Services.AddTransient(typeof(IBase<>), typeof(Base<>));

builder.Services.AddTransient(typeof(IFactoryUow), typeof(FactoryUow));

builder.Services.AddControllersWithViews().AddJsonOptions(opt =>
{
    opt.JsonSerializerOptions.PropertyNamingPolicy = null;
    opt.JsonSerializerOptions.AllowTrailingCommas = true;
    opt.JsonSerializerOptions.NumberHandling = JsonNumberHandling.AllowReadingFromString |
                                               JsonNumberHandling.WriteAsString;
    opt.JsonSerializerOptions.IncludeFields = true;
});


//IServiceProvider s = builder.Services;

//var T = Assembly.GetCallingAssembly().GetTypes()
//    .Where(w => typeof(IEntity).IsAssignableFrom(w) &&
//                w.IsClass && w is {BaseType: not null, IsInterface: false, IsAbstract: false}).ToList();

//foreach (var tt in T)
//{
//    var type1 = typeof(GetElementListQuery<>).MakeGenericType(tt,  );


//    builder.Services.AddTransient<IRequestHandler<GetElementListQuery<typeof(tt)>, List<tt>>, GetElementListHandler<tt>>();

//    //builder.Services.AddTransient < IRequestHandler < GetElementListQuery < typeof(tt) >, List < tt >>, GetElementListHandler < tt >> ();


//    builder.Services.Add(new ServiceDescriptor(
//            tt, // interface
//            (s) => Activator.CreateInstance(typeof(Base<>), tt) /*, other params */), // implementation
//        ServiceLifetime.Transient); // lifetime

//        builder.Services.AddTransient<IRequestHandler>(serviceProvider =>
//        {
//            // gather all the constructor parameters here
//            return new TheImplementation(/* pass in the constructor parameters */);
//        });
//}

////foreach (KeyValuePair<Type, Type> servicesType in servicesTypes)
////{
////    services.Add(new ServiceDescriptor(
////        serviceType.Key, // interface
////        serviceProvider => Activator.CreateInstance(servicesType.Value, Base<> /*, other params */), // implementation
////        ServiceLifetime.Transient); // lifetime
////}
////}

//var f = typeof(GetElementListHandler<>).MakeGenericType(typeof(Actor));

//var b = typeof(Base<>).MakeGenericType(typeof(Actor));

//var ggg = Activator.CreateInstance(b);

//var gg = Activator.CreateInstance(f, ggg);

//builder.Services.AddTransient<
//    IRequestHandler<GetElementListQuery<Actor>, List<Actor>>,
//    GetElementListHandler<Actor>>();


var app = builder.Build();


//var dbContext = app.ServiceProvider
//    .GetRequiredService<AppDbContext>();

//// Here is the migration executed
//dbContext.Database.Migrate();

//string J =
//    "{\"searchType\":\"Movie\",\"expression\":\"inception 2010\",\"results\":[{\"id\":\"tt1375666\",\"resultType\":\"Title\",\"image\":\"https://m.media-amazon.com/images/M/MV5BMjAxMzY3NjcxNF5BMl5BanBnXkFtZTcwNTI5OTM0Mw@@._V1_Ratio0.6800_AL_.jpg\",\"title\":\"Inception\",\"description\":\"(2010)\"},{\"id\":\"tt1790736\",\"resultType\":\"Title\",\"image\":\"https://m.media-amazon.com/images/M/MV5BMjE0NGIwM2EtZjQxZi00ZTE5LWExN2MtNDBlMjY1ZmZkYjU3XkEyXkFqcGdeQXVyNjMwNzk3Mjk@._V1_Ratio0.6800_AL_.jpg\",\"title\":\"Inception: The Cobol Job\",\"description\":\"(2010 Video)\"},{\"id\":\"tt5295990\",\"resultType\":\"Title\",\"image\":\"https://m.media-amazon.com/images/M/MV5BZGFjOTRiYjgtYjEzMS00ZjQ2LTkzY2YtOGQ0NDI2NTVjOGFmXkEyXkFqcGdeQXVyNDQ5MDYzMTk@._V1_Ratio0.6800_AL_.jpg\",\"title\":\"Inception: Jump Right Into the Action\",\"description\":\"(2010 Video)\"},{\"id\":\"tt1686778\",\"resultType\":\"Title\",\"image\":\"https://imdb-api.com/images/original/nopicture.jpg\",\"title\":\"Inception: 4Movie Premiere Special\",\"description\":\"(2010 TV Movie)\"},{\"id\":\"tt12960252\",\"resultType\":\"Title\",\"image\":\"https://imdb-api.com/images/original/nopicture.jpg\",\"title\":\"Inception Premiere\",\"description\":\"(2010)\"}],\"errorMessage\":\"\"}";

//dynamic H = JsonConvert.DeserializeObject(J) ?? "";

//if (string.IsNullOrWhiteSpace((string)H.errorMessage))
//{
//    var rowsResult = ((JArray)H.results)[0].Value<JObject>()!.ToObject<Movie>();
//}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.UseResponseCaching();

app.UseMiddleware<GlobalExceptionHandler>();

app.Run();