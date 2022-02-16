using BlazorNlog.Data;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using NLog;
using NLog.Extensions.Logging;
using NLog.Web;

//var logger = NLog.LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
//LogManager.ThrowExceptions = true;

//上面代码仔细看看,只记录Error级别, 记录到 "bin/logs"目录,配置文件 nlog.config minlevel="Error"

ILoggerFactory loggerFactory = LoggerFactory.Create(builder =>
{
builder
    .AddDebug()
    .AddConsole()
    .AddNLog();
});
Microsoft.Extensions.Logging.ILogger logger = loggerFactory.CreateLogger<Program>();


logger.LogDebug("init main Debug");
logger.LogWarning("init main Warn");
logger.LogError("init main Error");

try
{
    var builder = WebApplication.CreateBuilder(args);

    // Add services to the container.
    builder.Services.AddRazorPages();
    builder.Services.AddServerSideBlazor();
    builder.Services.AddSingleton<WeatherForecastService>();
    builder.Services.AddSingleton(logger);

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (!app.Environment.IsDevelopment())
    {
        app.UseExceptionHandler("/Error");
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
    }

    app.UseHttpsRedirection();

    app.UseStaticFiles();

    app.UseRouting();

    app.MapBlazorHub();
    app.MapFallbackToPage("/_Host");

    app.Run();
}
catch (Exception exception)
{
    // NLog: catch setup errors
    logger.LogError(exception, "Stopped program because of exception");
    throw;
}
finally
{
    // Ensure to flush and stop internal timers/threads before application-exit (Avoid segmentation fault on Linux)
    NLog.LogManager.Shutdown();
}