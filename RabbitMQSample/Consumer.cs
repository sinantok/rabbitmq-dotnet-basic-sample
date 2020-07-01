using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace RabbitMQSample
{
    public class Consumer
    {
        private readonly RabbitMQService _rabbitMQService;
        public Consumer(string queueName)
        {
            _rabbitMQService = new RabbitMQService();
            using (IConnection connection = _rabbitMQService.GetRabbitMQConnection())
            {
                using (IModel channel = connection.CreateModel())
                {
                    channel.QueueDeclare(
                        queue: queueName,
                        durable: false,
                        exclusive: false,
                        autoDelete: false,
                        arguments: null
                        );

                    var consumer = new EventingBasicConsumer(channel);
                    consumer.Received += (model, ea) =>
                    {// Received event'i sürekli listen modunda olacaktır.  
                        try
                        {
                            var body = ea.Body.Span;
                            string message = Encoding.UTF8.GetString(body);
                            List<Person> persons = JsonConvert.DeserializeObject<List<Person>>(message);

                            Console.WriteLine("{0} isimli queue üzerinden gelen mesaj geldi", queueName);
                            foreach (var item in persons)
                            {
                                Console.WriteLine("{0} isimli queue üzerinden gelen mesaj: \"{1}\"", queueName, $"{item.Name} {item.SurName}");
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("{0} consumer hata mesajı:", ex.Message);
                        }
                       
                    };
                   
                    channel.BasicConsume(
                        queue: queueName,
                        autoAck: true,
                        consumer: consumer);

                    Console.WriteLine("Veri alımı tamamlandı.");
                }
            }
        }
    }
}
