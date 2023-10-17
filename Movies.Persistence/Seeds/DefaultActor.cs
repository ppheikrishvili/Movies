using Bogus;
using Movies.Domain.Entity;

namespace Movies.Persistence.Seeds;

public sealed class DefaultActor : Faker<Actor>
{
    public DefaultActor()
    {
        Randomizer.Seed = new Random(8675309);
        RuleFor(p => p.Name, f => f.Name.FirstName());
    }
}