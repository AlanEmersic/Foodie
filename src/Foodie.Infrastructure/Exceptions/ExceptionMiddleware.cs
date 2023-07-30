using Foodie.Core.Exceptions;
using Microsoft.AspNetCore.Http;
using System.Net;

namespace Foodie.Infrastructure.Exceptions;

internal sealed class ExceptionMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception e)
        {
            await HandleExceptionAsync(context, e);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

        (int statusCode, ErrorDetails error) = exception switch
        {
            var ex when ex.GetType().IsGenericType && ex.GetType().GetGenericTypeDefinition() == typeof(EntityNotFoundException<>) => HandleEntityNotFoundException(ex),
            CustomException => (StatusCodes.Status400BadRequest, new ErrorDetails(StatusCodes.Status400BadRequest, nameof(CustomException).Replace("Exception", string.Empty), "Bad request.")),
            _ => (StatusCodes.Status500InternalServerError, new ErrorDetails(StatusCodes.Status500InternalServerError, "error", "There was an error."))
        };

        context.Response.StatusCode = statusCode;

        return context.Response.WriteAsJsonAsync(error);
    }

    private static (int, ErrorDetails) HandleEntityNotFoundException(Exception ex)
    {
        Type entityType = ex.GetType().GetGenericArguments()[0];
        string key = entityType.Name + "NotFound";

        return (StatusCodes.Status404NotFound, new ErrorDetails(StatusCodes.Status404NotFound, key, ex.Message));
    }

}
