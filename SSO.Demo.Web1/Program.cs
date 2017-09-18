using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace SSO.Demo.Web1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            /* var config = new ConfigurationBuilder()
           .SetBasePath(Directory.GetCurrentDirectory())
           .AddJsonFile("hosting.json", optional: true)
                .Build();*/

            BuildWebHost(args, null).Run();
        }

        public static IWebHost BuildWebHost(string[] args, IConfigurationRoot config) =>
            WebHost.CreateDefaultBuilder(args)
                .UseKestrel()
                 //.UseConfiguration(config)
                .UseContentRoot(Directory.GetCurrentDirectory())
                 .UseIISIntegration()
                 .UseStartup<Startup>()
                 .UseApplicationInsights()
                .Build();
    }
}
