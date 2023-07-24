using MediatR;
using Movies.Domain.Entity;
using Movies.Domain.Interface;
using System.Linq.Expressions;

namespace Movies.Application.Features.Queries
{
    public record GetSingleElementQuery<T>(Expression<Func<T, bool>>? condLambda) : IRequest<T>;

    public class GetSingleElementHandler<T> : IRequestHandler<GetSingleElementQuery<T>, T> where T : class, IEntity
    {
        private readonly IBase<T> _baseEntity;
        public GetSingleElementHandler(IBase<T> baseEntity) => _baseEntity = baseEntity;

        public async Task<T> Handle(GetSingleElementQuery<T> request, CancellationToken cancellationToken)
        {
            return await _baseEntity.FirstOrDefaultAsync(request.condLambda, cancellationToken) ??
                   throw new InvalidOperationException("Element not found");
        }
    }


    public class GetGetSingleElementHandlermdbUsers : GetSingleElementHandler<ImdbUser>
    {
        public GetGetSingleElementHandlermdbUsers(IBase<ImdbUser> baseEntity) : base(baseEntity)
        {
        }
    }
}