using System.Drawing.Text;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace MovieShopMVC.Infra
{
    public class MovieShopExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<MovieShopExceptionMiddleware> _logger;

        public MovieShopExceptionMiddleware(RequestDelegate next, ILogger<MovieShopExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            //read info from the HttpContext object and log it some where
            _logger.LogInformation("Inside exception Middleware");
            try
            {
                // If there is no exception then call next middleware
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception happened, handle here");

                // if there is exception then do the logging
                await HandleExceptionLogic(httpContext, ex);

            }
        }

        private async Task HandleExceptionLogic(HttpContext httpContext, Exception exception)
        {
            // How to handle exception: Create a ExceptionMiddleware,
            // First catch the exception
            // check the exception type, message,
            // check stacktrace, where the exception happened
            // when the exception happened
            // for which URL and which Http method ( controller, action method)
            // For which user, if user is logged in
            // Save all this information some where, text file, json file or database
            // asp.net has builtin logging mechanism,(ILogger) which can be used by any 3rd party log provide
            // *SeriLog* and Nlog
            // Send email to Dev Team when exception happens

            _logger.LogError("Something went wrong");
            var exceptionDetails = new
            {
                Message = exception.Message,
                // StackTrace = exception.StackTrace,
                ExceptionDateTime = DateTime.UtcNow,
                // ExceptionType = exception.GetType().ToString(),
                ExceptionDetails = exception.InnerException?.Message,
                Path = httpContext.Request.Path,
                HttpRequestType = httpContext.Request.Method,
                User = httpContext.User.Identity.IsAuthenticated ? httpContext.User.Identity.Name : null
            };
            
            // Next step: Log the above object details to text or json file using SeriLog
            // Then redirect to Error Page
            _logger.LogError(exceptionDetails.Message); 
            httpContext.Response.Redirect("/home/error");
            //Because we don't return anything so we do Task.CompletedTask
            await Task.CompletedTask;
        }
    }

    //Extension method used to add the middleware to the HTTP request pipeline
    public static class MovieShopExceptionMiddlewareExtensions
    {
        // Extension method
        public static IApplicationBuilder UserMovieShopExceptionMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<MovieShopExceptionMiddleware>();
        }
    }
}
