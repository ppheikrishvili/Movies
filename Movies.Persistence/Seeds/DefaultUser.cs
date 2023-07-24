using Bogus;
using Movies.Domain.Entity;

namespace Movies.Persistence.Seeds;

public sealed class DefaultUser : Faker<ImdbUser>
{
    public DefaultUser()
    {
        Randomizer.Seed = new Random(8675309);
        RuleFor(p => p.Name, f => f.Name.FirstName());
        RuleFor(p => p.Password, _ => Guid.NewGuid().ToString("d").Substring(5, 30));
        RuleFor(p => p.EMail, f => f.Internet.Email());
    }
}