using MediatR;
using Movies.Domain.Interface;
using System.Linq.Expressions;
using Movies.Domain.Entity;

namespace Movies.Application.Features.Queries;

public record GetElementListQuery<T>(Expression<Func<T, bool>>? condLambda = null) : IRequest<List<T>>;

public class GetElementListHandler<T> : IRequestHandler<GetElementListQuery<T>, List<T>> where T : class, IEntity
{
    private readonly IBase<T> _baseEntity;
    public GetElementListHandler(IBase<T> baseEntity) => _baseEntity = baseEntity;

    public async Task<List<T>> Handle(GetElementListQuery<T> request, CancellationToken cancellationToken)
    {
        return await _baseEntity.GetListAsync(request.condLambda, cancellationToken);
    }
}

public class GetElementListHandlerActor : GetElementListHandler<Actor>
{
    public GetElementListHandlerActor(IBase<Actor> baseEntity) : base(baseEntity)
    {
    }
}

public class GetElementListHandlerActorAward : GetElementListHandler<ActorAward>
{
    public GetElementListHandlerActorAward(IBase<ActorAward> baseEntity) : base(baseEntity)
    {
    }
}