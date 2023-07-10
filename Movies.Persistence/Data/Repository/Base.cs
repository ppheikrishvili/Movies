using System.Linq.Expressions;
using Movies.Domain.Interface;
using Movies.Domain.Shared.Enums;

namespace Movies.Persistence.Data.Repository;

public class Base<T> : IBase<T> where T : class, IEntity
{
    public Task<int> CountAsync(Expression<Func<T, bool>> condLambda)
    {
        throw new NotImplementedException();
    }

    public IQueryable<T> DbEntity()
    {
        throw new NotImplementedException();
    }

    public Task<List<T>> GetListAsync(Expression<Func<T, bool>>? condLambda, CancellationToken token = default)
    {
        throw new NotImplementedException();
    }

    public Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>>? condLambda = null, CancellationToken token = default)
    {
        throw new NotImplementedException();
    }

    public Task<IList<T>> SelectFullActiveList(CancellationToken token = default)
    {
        throw new NotImplementedException();
    }

    public Task<T?> ElementByIdAsync(object id)
    {
        throw new NotImplementedException();
    }

    public Task<bool> AnyAsync(Expression<Func<T, bool>>? condLambda = null)
    {
        throw new NotImplementedException();
    }

    public Task<bool> Delete(T baseEntity, Func<T, Task<bool>>? validateDelete = null)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteRange(Expression<Func<T, bool>> condLambda, CancellationToken token = default)
    {
        throw new NotImplementedException();
    }

    public Task<bool> Save(T baseEntity, InsertUpdateEnum isModified = InsertUpdateEnum.Update,
        Func<T, Task<bool>>? validateDelete = null)
    {
        throw new NotImplementedException();
    }

    public Task<bool> Save(IEnumerable<T> baseEntities, InsertUpdateEnum isModified = InsertUpdateEnum.Update)
    {
        throw new NotImplementedException();
    }

    public void Reload(T refreshItem)
    {
        throw new NotImplementedException();
    }
}