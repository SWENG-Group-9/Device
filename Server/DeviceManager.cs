using Microsoft.Azure.Devices;
using Microsoft.Azure.Devices.Common.Exceptions;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace register
{
    class manageDevices
    {
        static string tempID;
        static string deviceconn;

        const string connectionString ="HostName=sweng.azure-devices.net;SharedAccessKeyName=iothubowner;SharedAccessKey=7EVv/KvGgCAToj4TeHKRoo5I812ttE6I2O/wvnOUaJ4=";     
        public static void updateDeviceList()
        {
            GetDeviceIdAsync().Wait();
        }
        private static async Task GetDeviceIdAsync()
        {
            RegistryManager registryManager =
              RegistryManager.CreateFromConnectionString(connectionString);
            try
            {
                
                var query = registryManager.CreateQuery("SELECT * FROM devices", 100);
                while (query.HasMoreResults)
                {
                    var page = await query.GetNextAsTwinAsync();
                    foreach (var twin in page)
                    {
                        string id = JsonConvert.SerializeObject(twin.DeviceId);
                        Server.Program.devices.Add(id.Trim('"'));
                    }
                }
            }
            catch (DeviceAlreadyExistsException dvcEx)
            {
                Console.WriteLine("Error : {0}", dvcEx);
            }
        }

        public static string addDeviceEntrance(string newID)
        {
            deviceconn = "Unable to add device";
            tempID = newID;
            addDeviceToHub().Wait();
            return deviceconn;
        }

        public static async Task addDeviceToHub()
        {
            string deviceID = tempID;
            Device device;
            RegistryManager RM =
              RegistryManager.CreateFromConnectionString(connectionString);
            try
            {
                device = await RM.AddDeviceAsync(new Device(deviceID));
                Server.Program.devices.Add(deviceID);
                var dev = RM.GetDeviceAsync(deviceID).Result;
                deviceconn = dev.Authentication.SymmetricKey.PrimaryKey;
            }
            catch (DeviceAlreadyExistsException dvcEx)
            {
                Console.WriteLine("Error : {0}", dvcEx);
            }
        }
    }
}


