using System.Text.Json.Serialization;
using Movies.Domain.Interface;
using Movies.Domain.Shared.Enums;

namespace Movies.Domain.Entity;

public class ResponseResult<T>(string? eStr, ResponseCodeEnum respCode, T rValue) : IResponseResult<T>
{
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? ResponseStr { get; set; } = eStr;

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public T ReturnValue { get; set; } = rValue;

    public ResponseCodeEnum ResponseCode { get; set; } = respCode;

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public bool IsServiceError
    {
        get =>
            !string.IsNullOrWhiteSpace(ResponseStr) && (ResponseCode != ResponseCodeEnum.Notification) &&
            (ResponseCode != ResponseCodeEnum.Success);
        set { }
    }

    public ResponseResult(T rValue) : this(null, ResponseCodeEnum.Success, rValue)
    {
    }

    public ResponseResult(string? eStr, T rValue) : this(eStr, ResponseCodeEnum.Success, rValue)
    {
    }

    public ResponseResult(string? eStr, ResponseCodeEnum respCode ) : this(eStr, respCode, default!)
    {
    }

    public ResponseResult() : this(null, ResponseCodeEnum.Success, default!)
    {
    }

    public static ResponseResult<ICollection<TT>> ErrorResponseResultList<TT>(string eStr, ResponseCodeEnum respCode) =>
        new(eStr, respCode, Activator.CreateInstance<List<TT>>());

    public static ResponseResult<TT> ErrorResponseResultValue<TT>(string eStr, ResponseCodeEnum respCode) =>
        new(eStr, respCode, Activator.CreateInstance<TT>());
}