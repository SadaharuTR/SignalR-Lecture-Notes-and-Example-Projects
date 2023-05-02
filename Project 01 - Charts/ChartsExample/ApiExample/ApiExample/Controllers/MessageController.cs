using ApiExample.Models;
using Microsoft.AspNetCore.Connections;
using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Client;
using System;
using System.Text;
using System.Text.Json;

namespace ApiExample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : Controller
    {
        [HttpPost()]
        public IActionResult Post([FromForm]User model)
        {
            ConnectionFactory factory = new ConnectionFactory();
            factory.Uri = new Uri("amqps://odtlwoty:BLf5EDDMM_DQ_5d0dp6sP6f@whale.rmq.cloudamqp.com/odtlwoty");
            using IConnection connection =  factory.CreateConnection();
            using IModel channel = connection.CreateModel();

            channel.QueueDeclare("messagequeue", false, false, false);
            string serializeData = JsonSerializer.Serialize(model);

            byte[] data = Encoding.UTF8.GetBytes(serializeData);
            channel.BasicPublish("", "messagequeue", body: data);

            return Ok();
        }
    }
}
