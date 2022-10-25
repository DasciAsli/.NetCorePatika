using MiddlewarePractices.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseHello();//Custom middleware'ımızı ekledik

app.Run();


//BAZI MIDDLEWARELER ve ÇALIŞMA ŞEKİLLERİ

//app.Run()
//Bazı middlewareler (dotnet5'in kendi sunmuş olduğu middlewarelerden bazıları da) kendinden sonraki middleware'i çalıştırmaz.
//Buna middlewarelerle kısa devre yapmak denir.Akışta(pipelineda bir yerde Run metodu kullanırsanız bundan sonraki akışın çalışmayacağı anlamına geliyor.)
// app.Run(async context => Console.WriteLine("Middleware 1."));
// app.Run(async context => Console.WriteLine("Middleware 2."));





//app.Use
//Kendi işlemini yapıyor sonra next.invoke metoduyla bir sonrakine aktarım yapıyor.Bir sonraki middleware çalışsın diyor.
//Next.invoke'un altında bazı işlemler yapıyorsanız tüm akış tamamlandıktan sonra en son kaldığı yerden
// app.Use(async(context,next)=> //asenkron olması bağımsız çalışması anlamına geliyor
// {
//     Console.WriteLine("Middleware 1 başladı");
//     await next.Invoke();//await ile de asenkronsun sen ama buna rağmen işlem bittikten sonra diğer komutları çalıştırmaya başla demek istiyoruz.
//     Console.WriteLine("Middleware 1 sonlandırılıyor..");
// });

// app.Use(async(context,next)=>
// {
//     Console.WriteLine("Middleware 2 başladı");
//     await next.Invoke();
//     Console.WriteLine("Middleware 2 sonlandırılıyor..");


// });

// app.Use(async(context,next)=>
// {
//     Console.WriteLine("Middleware 3 başladı");
//     await next.Invoke();
//     Console.WriteLine("Middleware 3 sonlandırılıyor..");


// });





//app.Map
//Route'a göre middlewareleri yönetmemizi sağlıyor.
// app.Map("/example",internalApp =>
// {
//     internalApp.Run(async context =>
//     {
//         System.Console.WriteLine("/example middleware tetiklendi");
//         await context.Response.WriteAsync("/example middleware tetiklendi");//Context içerisindeki response'a mesaj yazmamızı sağlar.
//     });
// });// - /example routeuna bir istek gelirse o zaman bu middleware'ı çalıştır.





//app.MapWhen
//Eğer sadece path'e göre değil de reuestin içindeki bir parametreye göre middleware tetiklemek istersen
// app.MapWhen(x=>x.Request.Method == "GET",internalApp =>
// {
//     internalApp.Run(async context =>
//     {
//         System.Console.WriteLine("Mapwhen middleware tetiklendi");
//         await context.Response.WriteAsync("Mapwhen middleware tetiklendi");

//     });
// });




