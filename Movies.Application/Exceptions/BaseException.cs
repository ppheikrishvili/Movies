using Movies.Domain.Entity;
using Movies.Domain.Shared.Enums;
using Movies.Domain.Shared.Extensions;

namespace Movies.Application.Exceptions;

public class BaseException : Exception
{
    public ResponseResult<Exception> ResponseResult { get; }
    public BaseException(string message, ResponseCodeEnum responseCodeEnum) : base(message) => ResponseResult = new (message, responseCodeEnum);
    public BaseException(string message, Exception innerException, ResponseCodeEnum responseCodeEnum) : base(message, innerException) => ResponseResult = new(this.ToErrorStr(), responseCodeEnum);
}