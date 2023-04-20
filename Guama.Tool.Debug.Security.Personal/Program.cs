using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace MOROPO.Framework.Guama.Tool.Debug.Security.Personal
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) => WebHost.CreateDefaultBuilder(args).UseStartup<Startup>();
    }
}
