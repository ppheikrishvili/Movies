﻿using System.Collections.Concurrent;
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
        AppContext.Database.SetCommandTimeout(160);
    }

    //public FactoryUow()
    //{
    //    AppContext = new AppDBContext();
    //    AppContext.Database.SetCommandTimeout(160);
    //}

    private static Type? GetSubClassType(Type baseT) => Assembly.GetAssembly(baseT)?.GetTypes().FirstOrDefault(t =>
        t.IsSubclassOf(baseT) && t is {IsAbstract: false, IsClass: true});

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
        Type? repositoryType = GetSubClassType(typeof(Base<T>));
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