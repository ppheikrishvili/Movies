using Movies.Domain.Shared.Enums;

namespace Movies.Domain.Interface;

public interface IResponseResult<T>
{
    string? ResponseStr { get; set; }

    T ReturnValue { get; set; }

    ResponseCodeEnum ResponseCode { get; set; }

    bool IsServiceError { get; set; }
}