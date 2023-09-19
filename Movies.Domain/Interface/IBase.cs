using System.Linq.Expressions;
using Movies.Domain.Shared.Enums;

namespace Movies.Domain.Interface;

public interface IBase<T> where T : IEntity
{
    Task<int> CountAsync(Expression<Func<T, bool>> condLambda);

    IQueryable<T> DbEntity();

    Task<List<T>> GetListAsync(Expression<Func<T, bool>> condLambda, CancellationToken token = default);

    Task<List<T>> GetListAsync(CancellationToken token = default);

    Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> condLambda, CancellationToken token = default);

    Task<T?> FirstOrDefaultAsync(CancellationToken token = default);

    Task<T?> ElementByIdAsync(object id);

    Task<bool> AnyAsync(Expression<Func<T, bool>> condLambda );

    Task<bool> AnyAsync();

    Task DeleteAsync(T baseEntity, CancellationToken token = default);

    Task<int> DeleteAsync(Expression<Func<T, bool>> condLambda, CancellationToken token = default);

    Task DeleteAsync(IEnumerable<T> baseEntities, CancellationToken token = default);

    Task SaveAsync(T baseEntity, InsertUpdateEnum isModified, CancellationToken token = default);

    Task SaveAsync(IEnumerable<T> baseEntities, InsertUpdateEnum isModified, CancellationToken token = default);

    void Reload(T refreshItem);
}