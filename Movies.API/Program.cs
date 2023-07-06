using MediatR;
using Movies.Application.Behaviors;
using Movies.Application.Extension;
using Movies.Persistence.Common.Extension;


//https://learn.microsoft.com/en-us/dotnet/architecture/microservices/microservice-ddd-cqrs-patterns/microservice-application-layer-implementation-web-api

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersExt(builder);

builder.Services.AddFluent();
builder.Services.AddMedialR();

builder.Services.AddVersion();
builder.Services.AddMvcCore().AddApiExplorer();
builder.Services.AddVersionedApi();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwagger();

builder.Services.AddCorsExt();

builder.Services.AddResponseCaching();

builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

builder.Services.AddDbContext(builder.Configuration);


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

app.Run();