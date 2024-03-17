using MediatR;
using Movies.Domain.Entity;
using Movies.Domain.Interface;
using Movies.Domain.Shared.Enums;

namespace Movies.Application.Features.Commands;

public record SaveElementCommand<T>(T Element, InsertUpdateEnum InsertOrUpdate) : IRequest<ResponseResult<bool>>;

public class SaveElementCommandHandler<T>(IFactoryUow factoryUow)
    : IRequestHandler<SaveElementCommand<T>, ResponseResult<bool>>
    where T : class, IEntity
{
    public async Task<ResponseResult<bool>> Handle(SaveElementCommand<T> request, CancellationToken cancellationToken)
    {
        await factoryUow.Repository<T>().SaveAsync(request.Element, request.InsertOrUpdate, cancellationToken).ContinueWith( async (_) => await factoryUow.CommitAsync(cancellationToken), cancellationToken);
        return new ResponseResult<bool>( true);
    }
}