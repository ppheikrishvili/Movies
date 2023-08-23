using System.Linq.Expressions;
using Movies.Domain.Entity;
using Movies.Domain.Interface;
using Movies.Domain.Shared.Enums;

namespace Movies.Persistence.Data.Repository;

//public class ImdbUserData : IBase<ImdbUser>
//{
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