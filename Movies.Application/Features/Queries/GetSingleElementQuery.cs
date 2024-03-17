using MediatR;
using Movies.Application.Exceptions;
using Movies.Domain.Entity;
using Movies.Domain.Interface;
using System.Linq.Expressions;

namespace Movies.Application.Features.Queries
{
    public record GetSingleElementQuery<T>(Expression<Func<T, bool>>? CondLambda) : IRequest<ResponseResult<T>>;

    public class GetSingleElementHandler<T>(IFactoryUow baseEntity)
        : IRequestHandler<GetSingleElementQuery<T>, ResponseResult<T>>
        where T : class, IEntity
    {
        public async Task<ResponseResult<T>> Handle(GetSingleElementQuery<T> request, CancellationToken cancellationToken)
            => new(
                await baseEntity.Repository<T>().FirstOrDefaultAsync(request.CondLambda ?? ( w => true), cancellationToken) ??
                throw new ElementNotFoundException()
                );
    }
}