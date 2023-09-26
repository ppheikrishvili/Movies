using Movies.Domain.Shared.Enums;

namespace Movies.Application.Exceptions;

public class ElementNotFoundException : BaseException
{
    public ElementNotFoundException() : base("Element not found", ResponseCodeEnum.NotFound)
    { }
    public ElementNotFoundException(string message, Exception innerException) : base(message, innerException, ResponseCodeEnum.NotFound)
    { }
    public ElementNotFoundException(string message) : base(message, ResponseCodeEnum.NotFound)
    { }
}