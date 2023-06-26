using FluentValidation;
using Movies.Domain.Entity;

namespace Movies.Application.Validators;

public class ImdbUserValidation : AbstractValidator<ImdbUser>
{
    public ImdbUserValidation()
    {
        RuleFor(x => x.EMail).EmailAddress();
        RuleFor(x => x.Name).NotEmpty().MaximumLength(32);
    }
}