using FluentValidation.Results;
using Movies.Domain.Shared.Extensions;

namespace Movies.Application.Exceptions;

public class ValidationException : Exception
{
    public ValidationException() : base("One or more validation failures have occurred.")
    {
    }

    public List<string> Errors { get; } = new();

    public ValidationException(IEnumerable<ValidationFailure> failures)
        : this() => failures.AsParallel().ForAll(f => Errors.Add(f.ErrorMessage));

    public ValidationException(string message) : base(message) => Errors.Add(message);

    public ValidationException(string message, Exception innerException) : base(message, innerException) =>
        Errors.Add(this.ToErrorStr());
}