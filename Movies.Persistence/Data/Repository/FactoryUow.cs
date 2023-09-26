using System.Collections.Concurrent;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Movies.Domain.Interface;

namespace Movies.Persistence.Data.Repository;

public class FactoryUow : IFactoryUow, IAsyncDisposable
{
    public AppDBContext AppContext { get; set; }

    private ConcurrentDictionary<string, dynamic>? _repositories;

    public FactoryUow(AppDBContext appContext)
    {
        AppContext = appContext;
        //if (!AppContext.Database.IsInMemory()) AppContext.Database.SetCommandTimeout(160);
    }

    private static Type? GetSubClassType(Type baseT) => Assembly.GetCallingAssembly().GetTypes().FirstOrDefault(t =>
        baseT.IsAssignableFrom(t) && t is {IsAbstract: false, IsInterface: false});

    public async Task<int> CommitAsync(CancellationToken token = default) =>
        await AppContext.SaveChangesAsync(token).ConfigureAwait(false);

    async ValueTask IAsyncDisposable.DisposeAsync() => await AppContext.DisposeAsync().ConfigureAwait(false);

    public IBase<T> Repository<T>() where T : class, IEntity
    {
        _repositories ??= new ConcurrentDictionary<string, dynamic>();
        return _repositories.GetOrAdd(typeof(T).Name, _ => Repository<T>(AppContext));
    }

    private static IBase<T> Repository<T>(AppDBContext contextDb) where T : class, IEntity
    {
        Type? repositoryType = GetSubClassType(typeof(IBase<T>));
        if (repositoryType == null) return ((IBase<T>) Activator.CreateInstance(typeof(Base<T>), contextDb)!);
        return (IBase<T>) Activator.CreateInstance(repositoryType, contextDb)!;
    }

    public void RollBack()
    {
        foreach (EntityEntry entry in AppContext.ChangeTracker.Entries())
        {
            switch (entry.State)
            {
                case EntityState.Modified:
                    entry.State = EntityState.Unchanged;
                    break;
                case EntityState.Added:
                    entry.State = EntityState.Detached;
                    break;
                case EntityState.Deleted:
                    entry.Reload();
                    break;
            }
        }
    }
}