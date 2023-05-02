using Microsoft.AspNetCore.SignalR.Client;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace EmailSenderExample
{
    internal class Program
    {
        static async Task Main(string[] args)
        {

            HubConnection connectionSignalR = new HubConnectionBuilder().WithUrl("https://localhost:5003/messagehub")
                .Build();

            await connectionSignalR.StartAsync();

            ConnectionFactory factory = new ConnectionFactory();
            factory.Uri = new Uri("amqps://odtlwoty:BLf5EDDMMale.rmq.cloudamqp.com/odtlwoty");
            using IConnection connection = factory.CreateConnection();
            using IModel channel = connection.CreateModel();

            channel.QueueDeclare("messagequeue", false, false, false);
            
            EventingBasicConsumer consumer = new EventingBasicConsumer(channel);
            channel.BasicConsume("messagequeue", true, consumer);

            consumer.Received += async (s, e) =>
            {
                //email operation...
                //e.Body.Span
                string serializeData = Encoding.UTF8.GetString(e.Body.Span);
                User user = JsonSerializer.Deserialize<User>(serializeData);

                EmailSender.Send(user.Email, user.Message);
                Console.WriteLine($"{user.Email} 'e mail gönderilmiştir.");
                await connectionSignalR.InvokeAsync("SendMessageAsync", $"{user.Email} 'e mail gönderilmiştir.");
            };

            Console.Read();
        }
    }
}
