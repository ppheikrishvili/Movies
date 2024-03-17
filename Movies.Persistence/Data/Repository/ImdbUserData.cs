using Movies.Domain.Entity;

namespace Movies.Persistence.Data.Repository;

public class ImdbUserData(AppDbContext context) : Base<ImdbUser>(context)
{
    public override async Task<List<ImdbUser>> GetListAsync(CancellationToken token = default)
    {
        return await base.GetListAsync(token).ConfigureAwait(false);
    }
}