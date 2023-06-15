using FluentValidation;
using Movies.Domain.Entity;

namespace Movies.Application.Validators;

public class MovieValidation : AbstractValidator<Movie>
{
    public MovieValidation()
    {
        RuleFor(x => x.id).NotNull().NotEmpty();
        RuleFor(reg => reg.title).NotEmpty().MaximumLength(150);
        RuleFor(reg => reg.description).NotEmpty().MaximumLength(250);
    }
}