using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
namespace TeaManager.API.Middleware
{
    public class GlobalExceptionHandler : IExceptionHandler
    {

        private readonly ILogger<GlobalExceptionHandler> logger;
        public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
        {
            this.logger = logger;
        }
        public async ValueTask<bool> TryHandleAsync(
            HttpContext httpContext,
            Exception exception,
            CancellationToken cancellationToken)
        {
            var exceptionMessage = exception.Message;
            logger.LogError(
                "Error Message: {exceptionMessage}, Time of occurrence {time}",
                exceptionMessage, DateTime.UtcNow);
            int code;//statusCode
            string msg; // error message
            string errorDetail; //error details
            DateTime time; // timestamp
            if (exception is DbUpdateException || exception is InvalidOperationException)
            {
                code = 400;
                msg = "Bad Request";
                errorDetail = exception.Message;
                time = DateTime.UtcNow;
            }
            else
            {
                code = 500;
                msg = "Internal Server Error";
                errorDetail = exception.Message;
                time = DateTime.UtcNow;
            }
            httpContext.Response.StatusCode = code;
            httpContext.Response.ContentType = "application/json";
            await httpContext.Response.WriteAsJsonAsync(new
            {
                statusCode = code,
                message = msg,
                detail = errorDetail,
                timestamp = DateTime.UtcNow
            }
            );
            return true;
        }
    }
}