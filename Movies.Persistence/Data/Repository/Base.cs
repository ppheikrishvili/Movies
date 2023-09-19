using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Movies.Domain.Interface;
using Movies.Domain.Shared.Enums;

namespace Movies.Persistence.Data.Repository;

public class Base<T> : IBase<T> where T : class, IEntity
{
    public DbSet<T> DbSetEntity { get; set; }
    public AppDBContext AppContext { get; set; }

    public Base(AppDBContext context) => (AppContext, DbSetEntity) = (context, context.Set<T>());

    public async Task<int> CountAsync(Expression<Func<T, bool>> condLambda) =>
        await DbSetEntity.AsNoTracking().CountAsync(condLambda).ConfigureAwait(false);

    public IQueryable<T> DbEntity() => DbSetEntity;

    public async Task<List<T>> GetListAsync(Expression<Func<T, bool>> condLambda,
        CancellationToken token = default)
        => await DbSetEntity.AsNoTracking().Where(condLambda).ToListAsync(cancellationToken: token).ConfigureAwait(false);
    //await EF.CompileAsyncQuery((AppDBContext ctx, Expression<Func<T, bool>>? expression) =>
    //        ctx.Set<T>().AsNoTracking().Where(condLambda!).ToList())(AppContext, condLambda)
    //    .ConfigureAwait(false);

    public async Task<List<T>> GetListAsync(CancellationToken token = default)
    => await DbSetEntity.AsNoTracking().ToListAsync(cancellationToken: token).ConfigureAwait(false);
    //await EF.CompileAsyncQuery((AppDBContext ctx) => ctx.Set<T>().AsNoTracking().ToList())(AppContext)
    //    .ConfigureAwait(false);

    public async Task<T?> FirstOrDefaultAsync(CancellationToken token = default)
        => await EF.CompileAsyncQuery((AppDBContext ctx) => ctx.Set<T>().AsNoTracking().FirstOrDefault())(AppContext)
            .ConfigureAwait(false);

    public async Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> condLambda,
        CancellationToken token = default)
        => await EF.CompileAsyncQuery((AppDBContext ctx, Expression<Func<T, bool>> expression) =>
    ctx.Set<T>().AsNoTracking().FirstOrDefault(condLambda))(AppContext, condLambda)
    .ConfigureAwait(false);

    public async Task<T?> ElementByIdAsync(object id) => await DbSetEntity.FindAsync(id).ConfigureAwait(false);

    public async Task<bool> AnyAsync(Expression<Func<T, bool>> condLambda ) => await DbSetEntity.AsNoTracking().AnyAsync(condLambda).ConfigureAwait(false);

    public async Task<bool> AnyAsync() => await DbSetEntity.AsNoTracking().AnyAsync().ConfigureAwait(false);

    public async Task DeleteAsync(T baseEntity, CancellationToken token = default) =>
        await Task.Run(() => DbSetEntity.Remove(baseEntity), token).ConfigureAwait(false);

    public async Task<int> DeleteAsync(Expression<Func<T, bool>> condLambda, CancellationToken token = default) =>
         await DbSetEntity.Where(condLambda).ExecuteDeleteAsync(token).ConfigureAwait(false);

    public async Task DeleteAsync(IEnumerable<T> baseEntities, CancellationToken token = default) =>
        await Task.Run(() => DbSetEntity.RemoveRange(baseEntities), token).ConfigureAwait(false);

    public async Task SaveAsync(T baseEntity, InsertUpdateEnum isModified, CancellationToken token = default)
    {
        await Task.Run(() =>
            {
                AppContext.Entry(baseEntity).State = EntityState.Detached;
                _ = DbSetEntity.Attach(baseEntity);
                AppContext.Entry(baseEntity).State =
                    isModified == InsertUpdateEnum.Update ? EntityState.Modified : EntityState.Added;
            }, token).ConfigureAwait(false);
    }

    public async Task SaveAsync(IEnumerable<T> baseEntities, InsertUpdateEnum isModified, CancellationToken token = default)
    {
        var enumerable = baseEntities.ToList();
        if (isModified == InsertUpdateEnum.Update)
        {
            enumerable.AsParallel().ForAll(f => AppContext.Entry(f).State = EntityState.Detached);
            await AppContext.AddRangeAsync(enumerable, token).ConfigureAwait(false);
            enumerable.AsParallel().ForAll(f => AppContext.Entry(f).State = EntityState.Modified);
        }
        else await AppContext.AddRangeAsync(enumerable, token).ConfigureAwait(false);
    }

    public void Reload(T refreshItem) => AppContext.Entry(refreshItem).Reload();
}