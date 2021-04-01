using System;
using System.Threading.Tasks;
using Microsoft.Azure.Devices;

namespace InvokeDeviceMethod
{
    public class Program
    {
        private static ServiceClient s_serviceClient;
        
        // Connection string for your IoT Hub
        // az iot hub show-connection-string --hub-name {your iot hub name} --policy-name service
        public static string s_connectionString = "HostName=sweng.azure-devices.net;SharedAccessKeyName=service;SharedAccessKey=NTkLL9oPk7Gu8RrfadVc7+DoO9HnBvDWv9x311tv7nM=";

        public static async Task deviceMethod(string methodName,System.Collections.Generic.List<string> deviceID)
        {
            // This sample accepts the service connection string as a parameter, if present
            ValidateConnectionString();

            // Create a ServiceClient to communicate with service-facing endpoint on your hub.
            s_serviceClient = ServiceClient.CreateFromConnectionString(s_connectionString);

            await InvokeMethodAsync(methodName,deviceID);

            s_serviceClient.Dispose();
        }

        // Invoke the direct method on the device, passing the payload
        private static async Task InvokeMethodAsync(string method,System.Collections.Generic.List<string> iotDevice)
        {
            var methodInvocation = new CloudToDeviceMethod(method)
            {
                ResponseTimeout = TimeSpan.FromSeconds(30),
            };
            methodInvocation.SetPayloadJson("10");

            for(int i = 0;i < iotDevice.Count; i++){
                 // Invoke the direct method asynchronously and get the response from the simulated device.
                 try
                 {
                    var response = await s_serviceClient.InvokeDeviceMethodAsync(iotDevice[i], methodInvocation);
                    Console.WriteLine($"\nResponse status: {response.Status}, payload:\n\t{response.GetPayloadAsJson()}");
                 }
                  catch(Exception e)
                 {
                     //Console.WriteLine(e);
                 }
                
            }
        }

        private static void ValidateConnectionString()
        {
            try
            {
                _ = IotHubConnectionStringBuilder.Create(s_connectionString);
            }
            catch (Exception)
            {
                Console.WriteLine("Not a valid Service string");
            }
        }

        public static async Task setDoor()
        {
            while(true)
            {
                if(!Server.Program.manual)
                {
                    if(Server.Program.current<Server.Program.max)
                        {
                        Server.Program.locked = false;
                        await InvokeDeviceMethod.Program.deviceMethod("unlock",Server.Program.devices);
                    }
                    else
                    {
                        Server.Program.locked = true;
                        await InvokeDeviceMethod.Program.deviceMethod("lock",Server.Program.devices);
                    }
                }
            }
            
        }

        public static async Task overrideDoor()
        {
            if(Server.Program.manual)
            {
                Server.Program.manual = !Server.Program.manual;
                await setDoor();
            }
            else
            {
                if(Server.Program.locked)
                {
                    await InvokeDeviceMethod.Program.deviceMethod("unlock",Server.Program.devices);
                }
                else
                {
                    await InvokeDeviceMethod.Program.deviceMethod("lock",Server.Program.devices);
                }
                Server.Program.locked = !Server.Program.locked;
                Server.Program.manual = !Server.Program.manual;

            }

        }
    }
}