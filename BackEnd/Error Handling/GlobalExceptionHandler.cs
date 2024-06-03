using Newtonsoft.Json;
using System.Net;

public class GlobalExceptionHandler : IMiddleware
{
    private ILogger<GlobalExceptionHandler> _logger;

    public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
    {
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (ArgumentException ex)
        {
            string message = ex.Message.ToString();
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int) HttpStatusCode.BadRequest;
            // Log the Exception Details
            _logger.LogError($"Exception Details: {message}");
            var response = new ExceptionDetails
            {
                StatusCode = context.Response.StatusCode,
                Message = message,
            };
            await context.Response.WriteAsync(JsonConvert.SerializeObject(response));
        }
    }
}

public class ExceptionDetails
{
    public int StatusCode { get; set; }
    public string Message { get; set; }
}
