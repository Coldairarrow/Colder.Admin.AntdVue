using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System;

namespace Coldairarrow.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var path = AppDomain.CurrentDomain.BaseDirectory;
            Console.WriteLine(path);
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder
                        .UseUrls("http://*:5000")
                        .UseStartup<Startup>();
                });
    }
}
