using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using ApplicationCore.Models;

namespace MovieShopAPI.Middlewares
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
        
        // This is the piece of code that will be executed, put all the logic in here
        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                //read info from the HttpContext object and log it some where
                // If we don't catch any exception, we call the next middleware
                _logger.LogInformation("Inside the Middleware");
                await _next(httpContext);
            }
            // The catch block will execute when we get any exception
            catch (Exception ex)
            {
                // First catch the exception
                // check the exception type, message,
                // check stacktrace, where the exception happened
                // when the exception happened
                // for which URL and which Http method ( controller, action method)
                // For which user, if user is logged in
                // Save all this information some where (text file, json file or database) so we can diagnose it, find the reason and fix it later,
                // asp.net core has builtin logging mechanism,(ILogger) which can be used by any 3rd party log provider
                // *SeriLog* and Nlog implement ILogger interface
                // Send email to Dev Team when exception happens

                var exceptionDetails = new ErrorModel
                {
                    Message = ex.Message,
                    // StackTrace = ex.StackTrace,
                    ExceptionDateTime = DateTime.UtcNow,
                    // ExceptionType = ex.GetType().ToString(),
                    // Where the exception happened (URL)
                    Path = httpContext.Request.Path,
                    HttpRequestType = httpContext.Request.Method,
                    User = httpContext.User.Identity.IsAuthenticated ? httpContext.User.Identity.Name : null
                };
                // Right here, ASP.NET only log to console. When we use SeriLog, we need to log to text file 
                _logger.LogError("Exception happened, log this to text or Json files using SeriLog");
                
                //return http status code 500 (when exception happens)
                httpContext.Response.ContentType = "application/json";
                httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                var result = JsonSerializer.Serialize<ErrorModel>(exceptionDetails);
                await httpContext.Response.WriteAsync(result);
                
                // If MVC, httpContext.Response.Redirect("/home/error")
                
            }

        }
    }

    //Extension method on IApplicationBuilder, used to add the middleware to the HTTP request pipeline
    public static class MovieShopExceptionMiddlewareExtensions
    {
        public static IApplicationBuilder UserMovieShopExceptionMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<MovieShopExceptionMiddleware>();
        }
    }
}