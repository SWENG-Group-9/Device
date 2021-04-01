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
        public static int current = 0;
        public static int max = 10;
        public static List<string> devices = new List<string>();
        static ThreadStart hostTS = new ThreadStart(host);
        static string[] tempargs;

        public static bool locked = false;
        public static bool manual = false;

        public static void host()
        {
            CreateHostBuilder(tempargs).Build().Run();
        }

        public static async Task Main(string[] args)
        {
            tempargs = args;
            Thread hostT = new Thread(hostTS);
            hostT.Start();
            register.manageDevices.updateDeviceList();
            await InvokeDeviceMethod.Program.setDoor();
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