using FluentValidation;
using Movies.Domain.Entity;

namespace Movies.Application.Validators;

public class MovieValidation : AbstractValidator<Movie>
{
    public MovieValidation()
    {
        RuleFor(x => x.id).NotNull().NotEmpty();
        RuleFor(x => x.title).NotEmpty().MaximumLength(150);
        RuleFor(x => x.description).NotEmpty().MaximumLength(250);
    }
}