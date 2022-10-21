using Microsoft.AspNetCore.Mvc;

namespace HelloWebApi.Controllers;

[ApiController] //Bu attribute ile controller eylemlerinin bir response döneceğini taahhüt eder
[Route("[controller]")] //Bu endpointe bu controllerın ismi ile erişilir.
//Webapiye gelen requestlerin hangi controller tarafından karşılanacağını belirtiyor route

public class WeatherForecastController : ControllerBase //Default olarak ControllerBase sınıfından kalıtım alır controllerlar
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;
    //Logger implementasyonu yapılmış
    //Microsoft'un log alt yapısını kullanarak çalışıyor

    public WeatherForecastController(ILogger<WeatherForecastController> logger) //Dışarıdan bir log sınıfı inject edilmiş
    {
        _logger = logger;
    }



    //Geriye bir veri dönen get metodu bu metot.
    //Bu metot bir action metottur.
    //Bir resource üzerinde gerçekleştirilebilecek eylemlerdir action metotlar
    [HttpGet(Name = "GetWeatherForecast")] 
    public IEnumerable<WeatherForecast> Get()
    {
        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = DateTime.Now.AddDays(index),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        })
        .ToArray();
    }

    [HttpGet("{id}")] 
    public ActionResult<WeatherForecast> GetById(string id)
    {
        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = DateTime.Now.AddDays(index),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        })
        .ToArray()[0];
    }



}
