using System;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Receiver
{
	class Program
	{
		public const string HostName = "localhost";
		public const string MainQueueName = "main";

		static void Main(string[] args)
		{
			Console.WriteLine("Starting Receiver Console App");

			var queueFactory = new ConnectionFactory { HostName = HostName };
			while (true)
			{
				using (var queueConnection = queueFactory.CreateConnection())
				using (var queueChannel = queueConnection.CreateModel())
				{
					queueChannel.QueueDeclare(MainQueueName, false, false, false, arguments: null);

					var consumer = new EventingBasicConsumer(queueChannel);
					consumer.Received += (model, ea) =>
					{
						var messageBody = ea.Body;
						var messageDecoded = Encoding.UTF8.GetString(messageBody);
						Console.WriteLine($"time: {DateTime.Now.TimeOfDay}, message: {messageDecoded}");
					};

					queueChannel.BasicConsume(MainQueueName, true, consumer);
					Console.ReadLine();
				}
			}
		}
	}
}
