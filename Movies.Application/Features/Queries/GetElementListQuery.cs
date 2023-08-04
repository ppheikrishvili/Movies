using MediatR;
using Movies.Domain.Interface;
using System.Linq.Expressions;

namespace Movies.Application.Features.Queries;

//public record GetElementListQuery<T>(Expression<Func<T, bool>>? CondLambda = null) : IRequest<List<T>>;

//public class GetElementListHandler<T> : IRequestHandler<GetElementListQuery<T>, List<T>> where T : class, IEntity
//{
//    private readonly IBase<T> _baseEntity;
//    public GetElementListHandler(IBase<T> baseEntity) => _baseEntity = baseEntity;

//    public async Task<List<T>> Handle(GetElementListQuery<T> request, CancellationToken cancellationToken)
//    {
//        return await _baseEntity.GetListAsync(request.CondLambda, cancellationToken);
//    }
//}

public record GetElementListQuery<T>(Expression<Func<T, bool>>? CondLambda = null) : IRequest<List<T>>;

public class GetElementListHandler<T> : IRequestHandler<GetElementListQuery<T>, List<T>> where T : class, IEntity
{
    private readonly IFactoryUow _factoryUow;
    public GetElementListHandler(IFactoryUow baseEntity) => _factoryUow = baseEntity;

    public async Task<List<T>> Handle(GetElementListQuery<T> request, CancellationToken cancellationToken)
        => await _factoryUow.Repository<T>().GetListAsync(request.CondLambda, cancellationToken);
}