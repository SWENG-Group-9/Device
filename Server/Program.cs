using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Server
{
    public class Program
    {
        public static int current = 15;
        //maybe change this variable name to currentCustomers to avoid confusion with api calls
        public static int max = 10;
        static string[] devices = new string[]{"AZ-c89346886016"};
        static ThreadStart hostTS = new ThreadStart(host);
        static string[] tempargs;


        public static void host()
        {
            CreateHostBuilder(tempargs).Build().Run();
        }

        public static async Task Main(string[] args)
        {
            tempargs = args;
            Thread hostT = new Thread(hostTS);
            hostT.Start();
            await InvokeDeviceMethod.Program.deviceMethod("lock",devices);
            await devicemessages.Event.msg();
        }



        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}