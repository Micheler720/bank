using System.Diagnostics.CodeAnalysis;
using System.Net;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Bank.Core.Api;

[ExcludeFromCodeCoverage]
public abstract class MainController : ControllerBase
{
    protected ICollection<string> Errors = new List<string>();

    protected ActionResult CustomResponse(
        object? result = null, 
        HttpStatusCode httpStatusCode = HttpStatusCode.OK)
    {
        if (ValidOperation())
        {
            return httpStatusCode switch
            {
                HttpStatusCode.Created => Created("", result),
                HttpStatusCode.NoContent => NoContent(),
                _ => Ok(result),
            };
        }

        return BadRequest(new ValidationProblemDetails(new Dictionary<string, string[]>
        {
            { "Messages", Errors.ToArray() }
        }));
    }

    protected ActionResult CustomResponse(ModelStateDictionary modelState)
    {
        var errors = modelState.Values.SelectMany(e => e.Errors);
        foreach (var error in errors)
        {
            AddErrorToStack(error.ErrorMessage);
        }

        return CustomResponse();
    }

    protected ActionResult CustomResponse(ValidationResult validationResult)
    {
        foreach (var error in validationResult.Errors)
        {
            AddErrorToStack(error.ErrorMessage);
        }

        return CustomResponse(httpStatusCode: HttpStatusCode.Created);
    }

    protected bool ValidOperation()
    {
        return !Errors.Any();
    }

    protected void AddErrorToStack(string error)
    {
        Errors.Add(error);
    }

    protected void CleanErrors()
    {
        Errors.Clear();
    }
}