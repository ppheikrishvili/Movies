using Movies.Domain.Entity;

namespace Movies.Persistence.Data.Repository;

public class ImdbUserData : Base<ImdbUser>
{
    public ImdbUserData(AppDbContext context) : base(context)
    { }

    public override async Task<List<ImdbUser>> GetListAsync(CancellationToken token = default)
    {
        return await base.GetListAsync(token).ConfigureAwait(false);
    }
}