using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;

namespace RabbitMQSample
{
    public class RabbitMQService
    {
        private readonly string _hostName = "localhost";

        public IConnection GetRabbitMQConnection()
        {
            ConnectionFactory connectionFactory = new ConnectionFactory()
            {
                HostName = _hostName
            };
            return connectionFactory.CreateConnection();
        }
    }
}
