using System.Diagnostics;
using System.Net;
using System.Runtime.ExceptionServices;
using Microsoft.EntityFrameworkCore.Infrastructure.Internal;
using Newtonsoft.Json;

namespace WebApi.Middlewares
{
    public class CustomExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        public CustomExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var watch = Stopwatch.StartNew();//İzleme açıyor.Neyin ne kadar sürede çalıştığını izleyen bir timer mekanizması
            try
            {
                //Requestleri logluyoruz
                string message = "[Request]  HTTP" + context.Request.Method + "-" + context.Request.Path;
                System.Console.WriteLine(message);
                await _next(context); //Bu şekilde de bir sonraki middleware'ı çağırabiliriz.
                watch.Stop();//Request geldikten sonra response'a kadar ne kdr süre geçti?

                //Responseları logluyoruz
                message = "[Response] HTTP" + context.Request.Method + "-" + context.Request.Path + " responsed " + context.Response.StatusCode + " in " + watch.ElapsedMilliseconds + "ms";
                System.Console.WriteLine(message);

            }
            catch (System.Exception ex)
            {
                watch.Stop();
                await HandleException(context, ex, watch);
            }

        }

        private Task HandleException(HttpContext context, Exception ex, Stopwatch watch)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            string message = "[Error]    HTTP" + context.Request.Method + "-" + context.Response.StatusCode + " Error Message" + ex.Message + " in" + watch.Elapsed.TotalMilliseconds + "ms";
            System.Console.WriteLine(message);

            var result = JsonConvert.SerializeObject(new { error = ex.Message }, Formatting.None);
            return context.Response.WriteAsync(result);
        }
    }
    public static class CustomExceptionMiddlewareExtension
    {
        public static IApplicationBuilder UseCustomExceptionMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CustomExceptionMiddleware>();
        }
    }
}