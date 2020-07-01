using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;

namespace RabbitMQSample
{
    public class Publisher
    {
        private readonly RabbitMQService _rabbitMQService;

        public Publisher(string queueName, IList<Person> persons)
        {
            _rabbitMQService = new RabbitMQService();
            using (var connection = _rabbitMQService.GetRabbitMQConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(
                        queue: queueName,
                        durable: false,
                        exclusive: false,
                        autoDelete: false,
                        arguments: null
                        );
                    string jsonMessage = JsonConvert.SerializeObject(persons);
                    byte[] body = Encoding.UTF8.GetBytes(jsonMessage);

                    channel.BasicPublish(
                        exchange: "",
                        routingKey: queueName,
                        basicProperties: null,
                        body: body
                        );

                    Console.WriteLine("{0} queue'su üzerine mesaj yazıldı.", queueName);
                }
            }
        }
    }
}
