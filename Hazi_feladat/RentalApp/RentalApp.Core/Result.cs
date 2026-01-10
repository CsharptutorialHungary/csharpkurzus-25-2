

using System.Diagnostics;

namespace RentalApp.Core;

// ### EZT A KODOT A CALCULATOR IMPLEMENTACIOBOL MASOLTAM ###
internal class Result<TSuccess, TError>
{
    public bool IsSuccess { get; }
    public bool IsError => !IsSuccess;

    private TSuccess? _success { get; }
    private TError? _error { get; }

    public Result(TSuccess success)
    {
        IsSuccess = true;
        _success = success; 
    }

    public Result(TError error)
    {
        IsSuccess = false;
        _error = error;
    }

    public void Visit(Action<TSuccess> success, Action<TError> error)
    {
        if(IsSuccess && _success is not null)
        {
            success.Invoke(_success);
            return;
        }
        if(IsError && _error is not null)
        {
            error.Invoke(_error);
            return;
        }
        throw new UnreachableException("Result is in an invalid state.");
    }


}
