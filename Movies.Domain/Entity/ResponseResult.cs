using System.Text.Json.Serialization;
using Movies.Domain.Interface;
using Movies.Domain.Shared.Enums;

namespace Movies.Domain.Entity;

public class ResponseResult<T> : IResponseResult<T>
{
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? ResponseStr { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public T ReturnValue { get; set; }

    public ResponseCodeEnum ResponseCode { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public bool IsServiceError
    {
        get =>
            !string.IsNullOrWhiteSpace(ResponseStr) && (ResponseCode != ResponseCodeEnum.Notification) &&
            (ResponseCode != ResponseCodeEnum.Success);
        set { }
    }

    public ResponseResult(T rValue) =>
        (ReturnValue, ResponseStr, ResponseCode) = (rValue, null, ResponseCodeEnum.Success);

    public ResponseResult(string? eStr, T rValue) => (ReturnValue, ResponseStr) = (rValue, eStr);

    public ResponseResult(string? eStr, ResponseCodeEnum respCode, T rValue) =>
        (ReturnValue, ResponseStr, ResponseCode) = (rValue, eStr, respCode);

    public ResponseResult(string? eStr, ResponseCodeEnum respCode ) =>
        (ReturnValue, ResponseStr, ResponseCode) = (default!, eStr, respCode);

    public ResponseResult() => (ResponseStr, ReturnValue) = (null, default!);

    public static ResponseResult<ICollection<TT>> ErrorResponseResultList<TT>(string eStr, ResponseCodeEnum respCode) =>
        new(eStr, respCode, Activator.CreateInstance<List<TT>>());

    public static ResponseResult<TT> ErrorResponseResultValue<TT>(string eStr, ResponseCodeEnum respCode) =>
        new(eStr, respCode, Activator.CreateInstance<TT>());
}