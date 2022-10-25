namespace MiddlewarePractices.Middlewares
{
    public class HelloMiddleware
    {
        private readonly RequestDelegate _next;
        public HelloMiddleware(RequestDelegate next)
        {
            _next=next;
        }
        public async Task Invoke(HttpContext context)//async olan metotların geri dönüş tipleri Task olur
        {
            System.Console.WriteLine("Hello World");
            await _next.Invoke(context);
            System.Console.WriteLine("Bye World!");

        }
    }
    public static class HelloMiddlewareExtension
    {
        public static IApplicationBuilder UseHello(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<HelloMiddleware>();
        }

    }
}