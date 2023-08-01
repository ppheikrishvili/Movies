using MediatR;
using Movies.Domain.Interface;
using System.Text.Json;

namespace Movies.Application.Features.Queries;

public record GetMovieFromSourceQuery<T>(string CondLambda, string FactoryName, string RequestStr) : IRequest<List<T>>;

public class GetMovieFromSourceHandler<T> : IRequestHandler<GetMovieFromSourceQuery<T>, List<T>>
    where T : class, IEntity
{
    private readonly IHttpClientFactory iHttpClientFactory;

    public GetMovieFromSourceHandler(IHttpClientFactory _iHttpClientFactory) =>
        iHttpClientFactory = _iHttpClientFactory;

    public async Task<List<T>> Handle(GetMovieFromSourceQuery<T> request, CancellationToken cancellationToken)
    {
        var Client = iHttpClientFactory.CreateClient(request.FactoryName);
        using var response = await Client
            .GetAsync(request.RequestStr, HttpCompletionOption.ResponseHeadersRead, cancellationToken)
            .ConfigureAwait(false);
        response.EnsureSuccessStatusCode();
        var stream = await response.Content.ReadAsStreamAsync(cancellationToken).ConfigureAwait(false);
        return await JsonSerializer
            .DeserializeAsync<List<T>>(stream, new JsonSerializerOptions {PropertyNameCaseInsensitive = true},
                cancellationToken).ConfigureAwait(false) ?? new List<T>();
    }
}