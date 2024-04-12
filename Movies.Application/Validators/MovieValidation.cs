using FluentValidation;
using Movies.Domain.Entity;

namespace Movies.Application.Validators;

public class MovieValidation : AbstractValidator<Movie>
{
    public MovieValidation()
    {
        RuleFor(x => x.Id).NotNull().NotEmpty();
        RuleFor(x => x.Title).NotEmpty().MaximumLength(150);
        RuleFor(x => x.Description).NotEmpty().MaximumLength(250);
    }
}