using System.Net;

namespace DocumentManagement.Domain.Common;

public record Result(bool IsSuccess, IEnumerable<string>? Error, HttpStatusCode? StatusCode)
{
    public static Result Success() => new(true, null, null);
    public static Result Success(HttpStatusCode? StatusCode) => new(true, null, StatusCode);
    public static Result Fail(IEnumerable<string> Errors) => new(false, Errors, null);
    public static Result Fail(HttpStatusCode? StatusCode) => new(false, null, StatusCode);
    public static Result Fail(IEnumerable<string> Errors, HttpStatusCode? StatusCode) => new(false, Errors, StatusCode);
}

public record Result<TData>(TData? Content, bool IsSuccess, HttpStatusCode? StatusCode, IEnumerable<string>? Error) //: Result( IsSuccess, Error, StatusCode )
{
    public static Result<TData> Success(TData? content) => new(content, true, null, null);
    public static Result<TData> Success(TData? content, HttpStatusCode? StatusCode) => new(content, true, StatusCode, null);

    public static implicit operator Result<TData>(Result result) =>
        new(default, result.IsSuccess, result.StatusCode, result.Error);
    //public static Result<TData> Fail(HttpStatusCode? StatusCode) => new(default, false, StatusCode, null);
    //public static Result<TData> Fail(string? Error, HttpStatusCode? StatusCode ) => new(default, false, StatusCode, Error);
    //public static Result<TData> Fail(string? Error) => new(default, false, null, Error);
}
