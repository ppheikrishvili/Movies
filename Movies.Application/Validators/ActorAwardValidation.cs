using FluentValidation;
using Movies.Domain.Entity;

namespace Movies.Application.Validators;

public class ActorAwardValidation : AbstractValidator<ActorAward>
{
    public ActorAwardValidation()
    {
        RuleFor(x => x.ActorId).NotNull().NotEmpty().MaximumLength(32);
        RuleFor(x => x.MovieId).NotNull().NotEmpty().MaximumLength(32);
        RuleFor(x => x.AwardName).NotNull().NotEmpty().MaximumLength(32);
    }
}