using MediatR;
using Movies.Domain.Interface;
using Movies.Domain.Shared.Enums;

namespace Movies.Application.Features.Commands;

public record SaveElementCommand<T>(T Element, InsertUpdateEnum InsertOrUpdate) : IRequest<bool>;

public class SaveElementCommandHandler<T> : IRequestHandler<SaveElementCommand<T>, bool> where T : class, IEntity
{
    private readonly IBase<T> _baseEntity;
    public SaveElementCommandHandler(IBase<T> baseEntity) => _baseEntity = baseEntity;

    public async Task<bool> Handle(SaveElementCommand<T> request, CancellationToken cancellationToken)
    {
        await _baseEntity.SaveAsync(request.Element, request.InsertOrUpdate, cancellationToken);
        return true;
    }
}