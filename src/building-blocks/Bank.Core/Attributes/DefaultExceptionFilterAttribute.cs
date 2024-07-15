using Bank.Core.Communication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Serilog;

namespace Bank.Clients.Api.Attributes;

public class DefaultExceptionFilterAttribute : ExceptionFilterAttribute
{
    private const string DEFAULT_EXCEPTION = "Ocorreu um erro inesperado.";

    public override void OnException(ExceptionContext context)
    {
        Log.Error(context.Exception.Message);

        var errors = new ResponseErrorMessages
        {
            Messages = new List<string> { DEFAULT_EXCEPTION }
        };

        var responseResult = new ResponseResult()
        {
            Errors = errors
        };

        context.Result = new ObjectResult(responseResult)
        {
            StatusCode = responseResult.Status.GetHashCode()
        };
    }
}