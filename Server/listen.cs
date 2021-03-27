using System;
using System.Threading.Tasks;
using Azure.Messaging.EventHubs.Consumer;
using System.Text;
using System.Threading;
using System.Timers;
using Server;

namespace devicemessages
{
    public class Event
    { 
        private static System.Timers.Timer aTimer;
        private static string e_endpointName = "Endpoint=sb://iothub-ns-sweng-8642906-2c7633ebc9.servicebus.windows.net/;SharedAccessKeyName=iothubowner;SharedAccessKey=7EVv/KvGgCAToj4TeHKRoo5I812ttE6I2O/wvnOUaJ4=;EntityPath=sweng";
        public static string message = "";
        private static CancellationTokenSource __tokenSource = new CancellationTokenSource();
        private static CancellationToken ct = __tokenSource.Token;

        public static async Task msg(){
            message = "";
            await ReceiveMessagesFromDeviceAsync();
        }

        private static async Task ReceiveMessagesFromDeviceAsync()
        {   
            await using var consumer = new EventHubConsumerClient(
                EventHubConsumerClient.DefaultConsumerGroupName,
                e_endpointName);

            Console.WriteLine("Listening for messages on all partitions.");
            

            try
            {
                aTimer = new System.Timers.Timer(500);
                aTimer.Elapsed += OnTimedEvent;
                await foreach (PartitionEvent partitionEvent in consumer.ReadEventsAsync(ct))
                {
                    Console.WriteLine($"\nMessage received on partition {partitionEvent.Partition.PartitionId}:");
                    message = Encoding.UTF8.GetString(partitionEvent.Data.Body.ToArray());
                    if(message == "i"){
                        Program.current++;
                        Console.Write(Program.current);
                    }
                    else if(message == "d"){
                        Program.current--;
                        Console.Write(Program.current);
                    }
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