using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System;
using System.Net;
using Microsoft.AspNetCore.Server.Kestrel.Https;

namespace LettuceEncrypt4core
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();

                    webBuilder.UseKestrel(k =>
                    {
                        var appServices = k.ApplicationServices;
                        k.Listen(
                            IPAddress.Any, 443,
                            o => o.UseHttps(h =>
                            {
                                h.UseLettuceEncrypt(appServices);
                            }));
                    });


                    
                });
    }
}