using FluentValidation;
using Movies.Domain.Entity;

namespace Movies.Application.Validators;

public class ActorValidation : AbstractValidator<Actor>
{
    public ActorValidation()
    {
        RuleFor(x => x.id).NotNull().NotEmpty();
        RuleFor(x => x.name).NotEmpty().MaximumLength(150);
    }
}