using MediatR;
using Movies.Domain.Interface;
using System.Text.Json;

namespace Movies.Application.Features.Queries;

public record GetMovieFromSourceQuery<T>(string condLambda, string factoryName, string requestStr) : IRequest<List<T>>;

public class GetMovieFromSourceHandler<T> : IRequestHandler<GetMovieFromSourceQuery<T>, List<T>>
    where T : class, IEntity
{
    private readonly IHttpClientFactory iHttpClientFactory;

    public GetMovieFromSourceHandler(IHttpClientFactory _iHttpClientFactory) =>
        iHttpClientFactory = _iHttpClientFactory;

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