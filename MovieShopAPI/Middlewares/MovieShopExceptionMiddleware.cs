using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace MovieShopAPI.Middlewares
{
    public class MovieShopExtensionMiddleware
    {
        private readonly RequestDelegate _next;

        public MovieShopExtensionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public Task Invoke(HttpContext httpContext)
        {
            //read info from the HttpContext object and log it some where
            return _next(httpContext);
        }
    }

    //Extension method used to add the middleware to the HTTP request pipeline
    public static class MovieShopExceptionMiddlewareExtensions
    {
        public static IApplicationBuilder UserMovieShopExtensionMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<MovieShopExtensionMiddleware>();
        }
    }
}