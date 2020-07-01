using System;
using System.Collections.Generic;

namespace RabbitMQSample
{
    class Program
    {
        private static readonly string _queueName = "SinanTOK";
        private static Publisher _publisher;
        private static Consumer _consumer;
        static void Main(string[] args)
        {

            List<Person> persons = new List<Person>
            {
                new Person
                {
                    Id=1, Name = "Sinan", SurName = "TOK"

                }
            };

            _publisher = new Publisher(_queueName, persons);

            System.Threading.Thread.Sleep(5000);

            _consumer = new Consumer(_queueName);

            Console.ReadLine();
        }
    }
}
