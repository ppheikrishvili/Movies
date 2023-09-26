using FluentValidation.Results;
using Movies.Domain.Shared.Enums;

namespace Movies.Application.Exceptions;

public class ValidationException : BaseException
{
    public ValidationException() : base("One or more validation failures have occurred.", ResponseCodeEnum.IncorrectParameters)
    {}
    public ValidationException(IEnumerable<ValidationFailure> failures)
        : this() => failures.AsParallel().ForAll(f => ResponseResult.ResponseStr += f.ErrorMessage);
    public ValidationException(string message) : base(message, ResponseCodeEnum.IncorrectParameters)
    {}
    public ValidationException(string message, Exception innerException) : base(message, innerException, ResponseCodeEnum.IncorrectParameters) 
    {}
}