using Bogus;
using Movies.Domain.Entity;

namespace Movies.Persistence.Seeds;

public sealed class DefaultActor : Faker<Actor>
{
    public DefaultActor()
    {
        Randomizer.Seed = new Random(86750);
        RuleFor(p => p.Id, f => f.Hashids.Encode());
        RuleFor(p => p.Name, f => f.Name.FirstName());
    }
}