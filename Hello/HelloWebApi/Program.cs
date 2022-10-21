



/*
SERVİS AYARLARI
Uygulamamız içinde kullanacağımız bileşenlerin ayarlarını veriyoruz
Bileşenleri(servisleri) sınıflar,kütüphaneler,kod parçaları gibi düşünebiliriz.Belli bir işi yapmaktan sorumludurlar.
Uygulamanın kullanacağı servisleri burada ekliyoruz ve onları configure ediyoruz.
*/

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();





/*
CONFIGURE AYARLARI
Uygulamamıza gelen http isteklerinin hangi aşamalardan geçerek http cevabı oluşturacağını belirlediğimiz yer.
Genel olarak bir pipelinedan bahsedilebilir.Pipeline artarda gelen işlem sıralaması gibi düşünebilirsin.
Pipelineda sıra çok önemlidir.O yüzden doğru bir şekilde sıralama yapmak gerekir.
Middleware(Ara katman yazılımları) kullanılarak uygulama içerisinde bir pipeline oluşturuyoruz.
*/

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) //Development enviromentında çalışıyorsanız buradaki işlemleri yap
{
    app.UseSwagger();
    //Api'n requestlerini dökümente etmek için kullanılan bir arayüz sunan uygulamadır.
    //Bunu sadece development ortamında yapıyor.Kimse productionda kimse böyle birşeyi yapmak istemez.
    app.UseSwaggerUI();
}

app.UseHttpsRedirection(); //Httpye redirect(yönlendirmek) için kullanılır.

app.UseAuthorization();//Microsoftun kendi authorization middleware'i

app.MapControllers(); //Http requestler server'a geldiği anda nasıl maplenebileceği hakkındaki ayar

app.Run();
