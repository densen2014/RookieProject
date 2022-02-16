using Microsoft.Extensions.Logging;

namespace BlazorNlog.Data
{
    public class WeatherForecastService
    {
        private readonly ILogger _logger;

        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        public WeatherForecastService(ILogger logger)
        {
            _logger = logger;
            _logger.LogDebug(1, "NLog injected into WeatherForecastService");
            _logger.LogWarning("Test LogWarning!");
            _logger.LogError("Test LogError!");
            _logger.LogCritical("Test LogCritical!");
            _logger.LogInformation("Test LogInformation!");
            _logger.LogTrace("Test LogTrace!");
        }

        public Task<WeatherForecast[]> GetForecastAsync(DateTime startDate)
        {

            _logger.LogWarning("GetForecastAsync LogWarning!");
            _logger.LogError("GetForecastAsync LogError!");
            _logger.LogCritical("GetForecastAsync LogCritical!");
            _logger.LogInformation("GetForecastAsync LogInformation!");
            _logger.LogTrace("GetForecastAsync LogTrace!"); 

            return Task.FromResult(Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = startDate.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            }).ToArray());

        }
    }
}