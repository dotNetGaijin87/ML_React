using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace ML
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
                    webBuilder.UseKestrel(options => {
                           options.Limits.MaxRequestBodySize = null; // No limit on body size
                    })
                    .UseStartup<Startup>()
                    .UseIIS()
                    .UseIISIntegration();
                });
    }
}
