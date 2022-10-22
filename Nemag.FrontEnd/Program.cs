using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using System;
using System.Net;

[assembly: System.Reflection.AssemblyVersion("1.0.*")]
namespace Nemag.FrontEnd
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
                    if (OperatingSystem.IsLinux())
                        webBuilder = webBuilder
                            .UseKestrel()
                            .ConfigureKestrel(serverOptions =>
                            {
                                serverOptions.Limits.MaxRequestBodySize = long.MaxValue;
                                serverOptions.Listen(IPAddress.Any, 4000);
                            });
                    
                    webBuilder
                        .UseIISIntegration()
                        .UseStartup<Startup>();
                });
    }

    public static class Global
    {
        public static string AppVersion { get { return typeof(Program).Assembly.GetName().Version.ToString(); } }

        public static string JavascriptRepositorioUrl
        {
            get
            {
                return Startup.Configuration["JavascriptRepositorioUrl"]?.ToString();
            }
        }

        public static string ObterApiUrl(HttpRequest httpRequest)
        {
            var apiUrl = httpRequest.Scheme + "://" + httpRequest.Host.Host.Replace("webapp", "webapi") + (httpRequest.Host.Port.HasValue ? ":" + (httpRequest.Host.Port + 1000) : string.Empty);

            return apiUrl;
        }
    }
}
