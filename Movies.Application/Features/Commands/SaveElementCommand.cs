using MediatR;
using Movies.Domain.Entity;
using Movies.Domain.Interface;
using Movies.Domain.Shared.Enums;

namespace Movies.Application.Features.Commands;

public record SaveElementCommand<T>(T element, InsertUpdateEnum insertOrUpdate) : IRequest<bool>;

public class SaveElementCommandHandler<T> : IRequestHandler<SaveElementCommand<T>, bool> where T : class, IEntity
{
    private readonly IBase<T> _baseEntity;
    public SaveElementCommandHandler(IBase<T> baseEntity) => _baseEntity = baseEntity;

    public async Task<bool> Handle(SaveElementCommand<T> request, CancellationToken cancellationToken)
    {
        return await _baseEntity.Save(request.element, request.insertOrUpdate);
    }
}

public class SaveElementCommandActor : SaveElementCommandHandler<Actor>
{
    public SaveElementCommandActor(IBase<Actor> baseEntity) : base(baseEntity)
    {
    }
}