using System;
using System.Linq;
using System.Threading.Tasks;
using Azure.Messaging.EventHubs.Consumer;
using CommandLine;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Timers;
using server;

namespace devicemessages
{
    public class Event
    { 
        private static System.Timers.Timer aTimer;
        private static string s_connectionString = "HostName=sweng.azure-devices.net;SharedAccessKeyName=iothubowner;SharedAccessKey=7EVv/KvGgCAToj4TeHKRoo5I812ttE6I2O/wvnOUaJ4=";
        private static string e_endpointName = "Endpoint=sb://iothub-ns-sweng-8642906-2c7633ebc9.servicebus.windows.net/;SharedAccessKeyName=iothubowner;SharedAccessKey=7EVv/KvGgCAToj4TeHKRoo5I812ttE6I2O/wvnOUaJ4=;EntityPath=sweng";
        private static string eventHubName = "sweng";
        public static string message = "";
        private static CancellationTokenSource __tokenSource = new CancellationTokenSource();
        private static CancellationToken ct = __tokenSource.Token;

        public async void msg(){
            message = "";
            await ReceiveMessagesFromDeviceAsync();
        }

        private static async Task ReceiveMessagesFromDeviceAsync()
        {   
            
           
            await using var consumer = new EventHubConsumerClient(
                EventHubConsumerClient.DefaultConsumerGroupName,
                s_connectionString,
                eventHubName);

            Console.WriteLine("Listening for messages on all partitions.");
            

            try
            {
                aTimer = new System.Timers.Timer(500);
                aTimer.Elapsed += OnTimedEvent;
                await foreach (PartitionEvent partitionEvent in consumer.ReadEventsAsync(ct))
                {
                    Console.WriteLine($"\nMessage received on partition {partitionEvent.Partition.PartitionId}:");
                    message = Encoding.UTF8.GetString(partitionEvent.Data.Body.ToArray());
                }
            }
            catch (TaskCanceledException)
            {
                
            }
            finally
            {
                __tokenSource.Dispose();
            }
        }
        private static void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            __tokenSource.Cancel();
        }
    }
}