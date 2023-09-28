using Movies.Domain.Entity;

namespace Movies.Persistence.Data.Repository;

public class ImdbUserData : Base<ImdbUser>
{
    public ImdbUserData(AppDBContext context) : base(context)
    { }

    public override async Task<List<ImdbUser>> GetListAsync(CancellationToken token = default)
    {
        return await base.GetListAsync(token).ConfigureAwait(false);
    }
    //public override async Task<List<ImdbUser>> GetListAsync(CancellationToken token = default)
    //    => await base.GetListAsync(token).ConfigureAwait(false);
}
//    public Task<int> CountAsync(Expression<Func<ImdbUser, bool>> condLambda)
//    {
//        throw new NotImplementedException();
//    }

//    public IQueryable<ImdbUser> DbEntity()
//    {
//        throw new NotImplementedException();
//    }

//    public Task<List<ImdbUser>> GetListAsync(Expression<Func<ImdbUser, bool>>? condLambda = null,
//        CancellationToken token = default)
//    {
//        throw new NotImplementedException();
//    }

//    public Task<ImdbUser?> FirstOrDefaultAsync(Expression<Func<ImdbUser, bool>>? condLambda = null,
//        CancellationToken token = default)
//    {
//        throw new NotImplementedException();
//    }

//    public Task<ImdbUser?> ElementByIdAsync(object id)
//    {
//        throw new NotImplementedException();
//    }

//    public Task<bool> AnyAsync(Expression<Func<ImdbUser, bool>>? condLambda = null)
//    {
//        throw new NotImplementedException();
//    }

//    public Task<bool> Delete(ImdbUser baseEntity, Func<ImdbUser, Task<bool>>? validateDelete = null)
//    {
//        throw new NotImplementedException();
//    }

//    public Task<bool> DeleteRange(Expression<Func<ImdbUser, bool>> condLambda, CancellationToken token = default)
//    {
//        throw new NotImplementedException();
//    }

//    public Task<bool> Save(ImdbUser baseEntity, InsertUpdateEnum isModified = InsertUpdateEnum.Update,
//        Func<ImdbUser, Task<bool>>? validateDelete = null)
//    {
//        throw new NotImplementedException();
//    }

//    public Task<bool> Save(IEnumerable<ImdbUser> baseEntities, InsertUpdateEnum isModified = InsertUpdateEnum.Update)
//    {
//        throw new NotImplementedException();
//    }

//    public void Reload(ImdbUser refreshItem)
//    {
//        throw new NotImplementedException();
//    }
//}