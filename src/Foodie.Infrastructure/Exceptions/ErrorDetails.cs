namespace Foodie.Infrastructure.Exceptions;

internal sealed record ErrorDetails(int StatusCode, string Key, string Message);
