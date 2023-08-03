namespace Movies.Domain.Interface;

public interface IFactoryUow
{
    Task<int> CommitAsync(CancellationToken token = default);
    void RollBack();
    IBase<T> Repository<T>() where T : class, IEntity;
}