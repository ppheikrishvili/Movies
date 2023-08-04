using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Movies.Domain.Interface;
using Movies.Domain.Shared.Enums;

namespace Movies.Persistence.Data.Repository;

public class Base<T> : IBase<T> where T : class, IEntity
{
    public DbSet<T> DbSetEntity { get; set; }
    public AppDBContext AppContext { get; set; }

    //public Base()
    //{
    //    AppContext = new AppDBContext();
    //    DbSetEntity = AppContext.Set<T>();
    //}

    public Base(AppDBContext context) => (AppContext, DbSetEntity) = (context, context.Set<T>());

    public async Task<int> CountAsync(Expression<Func<T, bool>> condLambda) =>
        await DbSetEntity.AsNoTracking().CountAsync(condLambda).ConfigureAwait(false);

    public IQueryable<T> DbEntity() => DbSetEntity;

    public async Task<List<T>> GetListAsync(Expression<Func<T, bool>>? condLambda = null,
        CancellationToken token = default)
    {
        if (condLambda != null)
            return await DbSetEntity.AsNoTracking().Where(condLambda).ToListAsync(cancellationToken: token)
                .ConfigureAwait(false);
        return await DbSetEntity.AsNoTracking().ToListAsync(cancellationToken: token).ConfigureAwait(false);
    }

    public async Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>>? condLambda = null,
        CancellationToken token = default)
    {
        if (condLambda != null)
            return await DbSetEntity.AsNoTracking().FirstOrDefaultAsync(condLambda, token).ConfigureAwait(false);
        return await DbSetEntity.AsNoTracking().FirstOrDefaultAsync(token).ConfigureAwait(false);
    }

    public async Task<T?> ElementByIdAsync(object id) => await DbSetEntity.FindAsync(id).ConfigureAwait(false);

    public async Task<bool> AnyAsync(Expression<Func<T, bool>>? condLambda = null)
    {
        if (condLambda != null) return await DbSetEntity.AsNoTracking().AnyAsync(condLambda).ConfigureAwait(false);
        return await DbSetEntity.AsNoTracking().AnyAsync().ConfigureAwait(false);
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