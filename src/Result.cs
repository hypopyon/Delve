using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.ExceptionServices;

namespace Delve; 

public readonly struct Result {
    readonly bool success;
    readonly ExceptionDispatchInfo? exception;

    public Result() {
        success = false;
        exception = null;
    }
    public Result(Exception error) {
        success = false;
        exception = ExceptionDispatchInfo.Capture(error);
    }
    Result(bool success) {
        this.success = success;
        exception = null;
    }

    public static Result Failure => new();
    public static Result Success = new(true);
    public static Result FromError(Exception error) => new(error);

    [MemberNotNullWhen(false, nameof(Error))]
    public bool IsSuccessful => success && exception is null;

    private void Validate() => exception?.Throw();
    
    public Exception? Error => exception?.SourceException;

    public static bool operator &(in Result left, in Result right) =>
        left.success && left.exception is null && right.success && right.exception is null;

    public static bool operator true(in Result result) => result.success && result.exception is null;
    
    public static bool operator false(in Result result) => !result.success;

    public override string ToString() => exception?.SourceException.ToString() ?? "<NULL>";
}