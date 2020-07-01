using System;
using System.Collections.Generic;

namespace RabbitMQSample
{
    class Program
    {
        private static readonly string _queueName = "SinanTOK";
        private static Publisher _publisher;
        static void Main(string[] args)
        {

            List<Person> persons = new List<Person>
            {
                new Person
                {
                    Id=1,Name = "Sinan",SurName = "TOK", BirthDate = Convert.ToDateTime("14.08.1996")

                }
            };

            _publisher = new Publisher(_queueName, persons);

            Console.ReadLine();
        }
    }
}
