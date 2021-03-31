using System;
using System.Collections.Generic;
using System.IO;
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
        static string[] devices = new string[] {"az-c89346883a40","AZ-c89346886016"};
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
            statsToJson();                                                                      //calls function to write stats to .json file
            await InvokeDeviceMethod.Program.deviceMethod("lock", devices);
            await devicemessages.Event.msg();
        }



        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });

        public static void statsToJson()
        {
            DateTime date = DateTime.Now;                                                                                       //gets current date and time in format DD/MM/YYYY HH:MM:SS AM/PM
            File.WriteAllText(@"C:\Device\server\data.json", "{\"" + date.ToString("yyyy-MM-dd") + "\":[");                     //writes current date to .json file in format YYYY-MM-DD, with some syntax
            while (Equals(date.ToString("yyyy-MM-dd"), DateTime.Now.ToString("yyyy-MM-dd")))                                    //while still current day
            {
                String timeAndCurrent = null;                                                                                   //initialise output string
                if (Equals(date.ToString("t"), "23:30")){                                                                       //slight change in syntax for final output
                    timeAndCurrent = "{\"time\":\"" + date.ToString("t") + "\",\n\"value\":" + current + "},]\n";                       
                }
                if (Equals((date.ToString("t")[3] + date.ToString("t")[4]), "00") ||                                            //if time ends in 00 or 30 ie outputs time and value for current every half an hour
                (Equals((date.ToString("t")[3] + date.ToString("t")[4]), "30")))
                {
                    timeAndCurrent = "{\"time\":\"" + date.ToString("t") + "\",\n\"value\":" + current + "},\n";                //creates string with time in format HH:MM and value for current
                }
                File.WriteAllText(@"C:\Device\server\data.json", timeAndCurrent);                                               //writes output file to .json
            }
        }
    }
}

//String array, store time and current with {"} etc
