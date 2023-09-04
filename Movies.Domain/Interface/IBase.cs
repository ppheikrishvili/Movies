using System.Linq.Expressions;
using Movies.Domain.Shared.Enums;

namespace Movies.Domain.Interface;

public interface IBase<T> where T : IEntity
{
    Task<int> CountAsync(Expression<Func<T, bool>> condLambda);

    IQueryable<T> DbEntity();

    Task<List<T>> GetListAsync(Expression<Func<T, bool>>? condLambda, CancellationToken token = default);

    Task<List<T>> GetListAsync(CancellationToken token = default);

    Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> condLambda, CancellationToken token = default);

    Task<T?> FirstOrDefaultAsync(CancellationToken token = default);

    Task<T?> ElementByIdAsync(object id);

    Task<bool> AnyAsync(Expression<Func<T, bool>>? condLambda = null);

    Task<bool> Delete(T baseEntity, Func<T, Task<bool>>? validateDelete = null);

    Task<bool> DeleteRange(Expression<Func<T, bool>> condLambda, CancellationToken token = default);

    Task<bool> Save(T baseEntity, InsertUpdateEnum isModified = InsertUpdateEnum.Update,
        Func<T, Task<bool>>? validateDelete = null);

    Task<bool> Save(IEnumerable<T> baseEntities, InsertUpdateEnum isModified = InsertUpdateEnum.Update);

    void Reload(T refreshItem);
}