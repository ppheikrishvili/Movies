using MediatR;
using Movies.Domain.Entity;
using Movies.Domain.Interface;
using Movies.Domain.Shared.Enums;

namespace Movies.Application.Features.Commands;

public record SaveElementCommand<T>(T Element, InsertUpdateEnum InsertOrUpdate) : IRequest<ResponseResult<bool>>;

public class SaveElementCommandHandler<T> : IRequestHandler<SaveElementCommand<T>, ResponseResult<bool>> where T : class, IEntity
{
    private readonly IFactoryUow _factoryUow;
    public SaveElementCommandHandler(IFactoryUow factoryUow) => _factoryUow = factoryUow;

    public async Task<ResponseResult<bool>> Handle(SaveElementCommand<T> request, CancellationToken cancellationToken)
    {
        await _factoryUow.Repository<T>().SaveAsync(request.Element, request.InsertOrUpdate, cancellationToken).ContinueWith( async (_) => await _factoryUow.CommitAsync(cancellationToken), cancellationToken);
        return new ResponseResult<bool>( true);
        //await _baseEntity.SaveAsync(request.Element, request.InsertOrUpdate, cancellationToken);
        //return true;
    }
}