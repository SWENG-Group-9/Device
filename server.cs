using System;
using System.Linq;
using System.Threading.Tasks;
using devicemessages;
using InvokeDeviceMethod;

namespace server
{
    public class serv
    {
        static string[] devices = new string[]{"AZ-c89346886016"};
        public static async Task Main(String[] args)
        {
            await InvokeDeviceMethod.Program.deviceMethod("unlock",devices);
        }
    }
    
}