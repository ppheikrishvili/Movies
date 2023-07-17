using System.Linq.Expressions;
using MediatR;
using Movies.Domain.Entity;
using Movies.Domain.Interface;
using System.Text.Json;

namespace Movies.Application.Features.Queries;

public record GetMovieFromSourceQuery<T>(string condLambda, string factoryName, string requestStr) : IRequest<List<T>>;

public class GetMovieFromSourceHandler<T> : IRequestHandler<GetMovieFromSourceQuery<T>, List<T>>
    where T : class, IEntity
{
    private readonly IHttpClientFactory iHttpClientFactory;

    //private readonly IBase<T> _baseEntity;
    public GetMovieFromSourceHandler(IHttpClientFactory _iHttpClientFactory) =>
        iHttpClientFactory = _iHttpClientFactory;

    //=> _baseEntity = baseEntity;

    public async Task<List<T>> Handle(GetMovieFromSourceQuery<T> request, CancellationToken cancellationToken)
    {
        var Client = iHttpClientFactory.CreateClient(request.factoryName);

        using var response = await Client.GetAsync(request.requestStr, HttpCompletionOption.ResponseHeadersRead,
            cancellationToken);
        response.EnsureSuccessStatusCode();
        var stream = await response.Content.ReadAsStreamAsync(cancellationToken);

        return await JsonSerializer.DeserializeAsync<List<T>>(stream,
            new JsonSerializerOptions {PropertyNameCaseInsensitive = true}, cancellationToken) ?? new List<T>();
    }
}

public class GetMovieFromSourceHandlerActor : GetMovieFromSourceHandler<Actor>
{
    public GetMovieFromSourceHandlerActor(IHttpClientFactory _iHttpClientFactory) : base(_iHttpClientFactory)
    {
    }
}

public class GetMovieFromSourceHandlerMovie : GetMovieFromSourceHandler<Movie>
{
    public GetMovieFromSourceHandlerMovie(IHttpClientFactory _iHttpClientFactory) : base(_iHttpClientFactory)
    {
    }
}


////public record GetMovieFromSourceQuery<T>
////    (string condLambda, string factoryName, string requestStr) : IRequest<List<T>>;

//public record GetMovieFromSourceQuery<T> : IRequest<List<T>>;

//public class GetMovieFromSourceHandler<T> : IRequestHandler<GetMovieFromSourceQuery<T>, List<T>>
//    where T : class, IEntity
//{
//    //private readonly IHttpClientFactory iHttpClientFactory;

//    //public GetMovieFromSourceHandler(IHttpClientFactory _iHttpClientFactory) =>
//    //    iHttpClientFactory = _iHttpClientFactory;

//    public GetMovieFromSourceHandler(IBase<T> baseEntity)
//    {
//    }


//    public Task<List<T>> Handle(GetMovieFromSourceQuery<T> request, CancellationToken cancellationToken)
//    {
//        //var Client = iHttpClientFactory.CreateClient(request.factoryName);

//        //using var response = await Client.GetAsync(request.requestStr, HttpCompletionOption.ResponseHeadersRead,
//        //    cancellationToken);
//        //response.EnsureSuccessStatusCode();
//        //var stream = await response.Content.ReadAsStreamAsync(cancellationToken);
//        //return await JsonSerializer.DeserializeAsync<List<T>>(stream,
//        //    new JsonSerializerOptions {PropertyNameCaseInsensitive = true}, cancellationToken) ?? new List<T>();
//        return Task.FromResult(new List<T>());
//    }


//    public class GetMovieFromSourceHandlerMovie : GetMovieFromSourceHandler<Movie>
//    {
//        //public GetMovieFromSourceHandlerMovie(IHttpClientFactory _iHttpClientFactory) : base(_iHttpClientFactory)
//        //{
//        //}
//        public GetMovieFromSourceHandlerMovie(IBase<Movie> baseEntity) : base(baseEntity)
//        {
//        }
//    }


//    public class GetMovieFromSourceHandlerActor : GetMovieFromSourceHandler<Actor>
//    {
//        //public GetMovieFromSourceHandlerMovie(IHttpClientFactory _iHttpClientFactory) : base(_iHttpClientFactory)
//        //{
//        //}
//        public GetMovieFromSourceHandlerActor(IBase<Actor> baseEntity) : base(baseEntity)
//        {
//        }
//    }
//}