using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using LettuceEncrypt;

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
                    //webBuilder.UseKestrel(k =>
                    //{
                    //    var appServices = k.ApplicationServices;
                    //    k.Listen(
                    //        IPAddress.Any, 443,
                    //        o => o.UseHttps(h =>
                    //        {
                    //            h.UseLettuceEncrypt(appServices);
                    //        }));
                    //});
                    webBuilder.UseStartup<Startup>();

                });


    }
}
