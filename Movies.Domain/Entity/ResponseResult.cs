﻿using System.Text.Json.Serialization;
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
        get
        {
            if (string.IsNullOrWhiteSpace(ResponseStr)) return false;
            if ((ResponseCode != ResponseCodeEnum.Notification) && (ResponseCode != ResponseCodeEnum.Success))
                return true;
            return false;
        }
        set { }
    }

    public ResponseResult(T rValue) =>
        (ReturnValue, ResponseStr, ResponseCode) = (rValue, "", ResponseCodeEnum.Success);

    public ResponseResult(string? eStr, T rValue) => (ReturnValue, ResponseStr) = (rValue, eStr);

    public ResponseResult(string? eStr, ResponseCodeEnum respCode, T rValue) =>
        (ReturnValue, ResponseStr, ResponseCode) = (rValue, eStr, respCode);

    public ResponseResult() => (ResponseStr, ReturnValue) = ("", default!);

    public static ResponseResult<ICollection<TT>> ErrorResponseResultList<TT>(string eStr, ResponseCodeEnum respCode) =>
        new ResponseResult<ICollection<TT>>(eStr, respCode, Activator.CreateInstance<List<TT>>());

    public static ResponseResult<TT> ErrorResponseResultValue<TT>(string eStr, ResponseCodeEnum respCode) =>
        new ResponseResult<TT>(eStr, respCode, Activator.CreateInstance<TT>());
}