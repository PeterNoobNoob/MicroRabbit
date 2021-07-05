using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;

namespace Consumer
{
    public class Receiver
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connetion = factory.CreateConnection())
            using (var channel = connetion.CreateModel())
            {
                channel.QueueDeclare("BasicTest", false, false, false, null);

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body;
                    var message = Encoding.UTF8.GetString(body);
                    Console.WriteLine("received mesage {0}...", message);
                };


                channel.BasicConsume("BasicTest", true, consumer);

                Console.WriteLine("Press [enter] to exit the Consumer...");
                Console.ReadLine();
            
            
            }
        
        
        }
    }
}
