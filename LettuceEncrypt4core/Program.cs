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

                    //webBuilder.UseKestrel(k =>
                    //{
                    //    var appServices = k.ApplicationServices;
                    //    k.ConfigureHttpsDefaults(h =>
                    //    {
                    //        h.ClientCertificateMode = ClientCertificateMode.RequireCertificate;
                    //        h.UseLettuceEncrypt(appServices);
                    //    });
                    //});

                    webBuilder.UseKestrel(k =>
                    {
                        var appServices = k.ApplicationServices;
                        k.Listen(
                            IPAddress.Any, 5001,
                            o => o.UseHttps(h =>
                            {
                                h.UseLettuceEncrypt(appServices);
                            }));
                    });

                    // This example shows how to configure Kestrel's client certificate requirements along with
                    // enabling Lettuce Encrypt's certificate automation.
                    if (Environment.GetEnvironmentVariable("REQUIRE_CLIENT_CERT") == "true")
                    {
                        webBuilder.UseKestrel(k =>
                        {
                            var appServices = k.ApplicationServices;
                            k.ConfigureHttpsDefaults(h =>
                            {
                                h.ClientCertificateMode = ClientCertificateMode.RequireCertificate;
                                h.UseLettuceEncrypt(appServices);
                            });
                        });
                    }

                    // This example shows how to configure Kestrel's address/port binding along with
                    // enabling Lettuce Encrypt's certificate automation.
                    if (Environment.GetEnvironmentVariable("CONFIG_KESTREL_VIA_CODE") == "true")
                    {
                        webBuilder.PreferHostingUrls(false);
                        webBuilder.UseKestrel(k =>
                        {
                            var appServices = k.ApplicationServices;
                            k.Listen(IPAddress.Any, 443,
                                o =>
                                    o.UseHttps(h => h.UseLettuceEncrypt(appServices)));
                        });
                    }
                });

    }
}