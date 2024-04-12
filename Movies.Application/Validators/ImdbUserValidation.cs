using FluentValidation;
using Movies.Domain.Entity;

namespace Movies.Application.Validators;

public class ImdbUserValidation : AbstractValidator<ImdbUser>
{
    public ImdbUserValidation()
    {
        RuleFor(x => x.EMail).EmailAddress();
        RuleFor(x => x.Name).NotEmpty().MaximumLength(32);

        RuleFor(p => p.Password).NotEmpty().WithMessage("Your password cannot be empty")
            .MinimumLength(5).WithMessage("Your password length must be at least 5.")
            .MaximumLength(126).WithMessage("Your password length must not exceed 126.")
            .Matches("[A-Z]+").WithMessage("Your password must contain at least one uppercase letter.")
            .Matches("[a-z]+").WithMessage("Your password must contain at least one lowercase letter.")
            .Matches("[0-9]+").WithMessage("Your password must contain at least one number.")
            .Matches("[^a-zA-Z0-9]+").WithMessage("Your password must contain at least one (!@#$%^&*).");
    }
}