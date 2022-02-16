using Microsoft.Extensions.Logging;

namespace BlazorNlog.Data
{
    public class WeatherForecastService
    {
        private readonly ILogger<WeatherForecastService> _logger;

        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        public WeatherForecastService(ILogger<WeatherForecastService> logger)
        {
            _logger = logger;
            _logger.LogDebug(1, "NLog injected into WeatherForecastService");
        }

        public Task<WeatherForecast[]> GetForecastAsync(DateTime startDate)
        {

            Console.Error.WriteLine("sssssss");

            _logger.LogInformation("Hello, GetForecastAsync!");
            _logger.LogError("Test Error, GetForecastAsync!");
            return Task.FromResult(Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = startDate.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            }).ToArray());

        }
    }
}