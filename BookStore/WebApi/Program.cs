using System.Reflection;
using Microsoft.EntityFrameworkCore;
using WebApi.DBOperations;
using WebApi.Middlewares;
using WebApi.Services;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddDbContext<BookStoreDbContext>(options=>options.UseInMemoryDatabase(databaseName:"BookStoreDB"));
        builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());//Automapper'ı servis olarak kullanabilmek için ekliyoruz.
        builder.Services.AddSingleton<ILoggerService,DBLogger>();
       


        var app = builder.Build();
         using (var scope = app.Services.CreateScope())
         {
             var services = scope.ServiceProvider;
             DataGenerator.Initialize(services);
         }

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.UseCustomExceptionMiddleware();//Custom middleware

        app.MapControllers();

        app.Run();
    }
}