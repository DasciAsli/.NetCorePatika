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

         //InMemoryDatabase
        builder.Services.AddDbContext<BookStoreDbContext>(options=>options.UseInMemoryDatabase(databaseName:"BookStoreDB"));
       
        //Automapper'ı servis olarak kullanabilmek için ekliyoruz.
        builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
        
        //Singleton,bu yaşam süresine sahip nesne, uygulamanın çalışmaya başladığı andan duruncaya kadar geçen tüm süre boyunca yalnızca bir kez oluşturulur ve her zaman aynı nesne kullanılır.
        builder.Services.AddSingleton<ILoggerService,DBLogger>();

        //Scope, inject edilen servisin sadece request geldiğinde instance'ini oluşturuyor ve o requeste istinaden bir response dönene kadar varlığını sürdürüyor.Response dönünce yok ediliyor.
        builder.Services.AddScoped<IBookStoreDbContext>(provider =>provider.GetService<BookStoreDbContext>());//Bunu inject ettiğim yerlerde bu servis BookStoreDbContext'e karşılık geliyor.


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

        //Custom middleware
        app.UseCustomExceptionMiddleware();

        app.MapControllers();

        app.Run();
    }
}