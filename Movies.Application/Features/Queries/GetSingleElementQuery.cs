using MediatR;
using Movies.Domain.Entity;
using Movies.Domain.Interface;
using System.Linq.Expressions;

namespace Movies.Application.Features.Queries
{
    public record GetSingleElementQuery<T>(Expression<Func<T, bool>>? CondLambda) : IRequest<ResponseResult<T>>;

    public class GetSingleElementHandler<T> : IRequestHandler<GetSingleElementQuery<T>, ResponseResult<T>>
        where T : class, IEntity
    {
        private readonly IFactoryUow _factoryUow;
        public GetSingleElementHandler(IFactoryUow baseEntity) => _factoryUow = baseEntity;

        public async Task<ResponseResult<T>> Handle(GetSingleElementQuery<T> request, CancellationToken cancellationToken)
            => new(
                await _factoryUow.Repository<T>().FirstOrDefaultAsync(request.CondLambda!, cancellationToken) ??
                throw new InvalidOperationException("Element not found"));
    }
}