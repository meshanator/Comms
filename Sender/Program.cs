using System;
using System.Text;
using RabbitMQ.Client;

namespace Sender
{
	class Program
	{
		public const string HostName = "localhost";
		public const string MainQueueName = "main";

		static void Main(string[] args)
		{
			Console.WriteLine("Starting Sender Console App");

			var queueFactory = new ConnectionFactory { HostName = HostName };
			while (true)
			{
				using (var queueConnection = queueFactory.CreateConnection())
				using (var queueChannel = queueConnection.CreateModel())
				{
					queueChannel.QueueDeclare(MainQueueName, false, false, false, null);

					var messageToSend = Console.ReadLine();
					var encodedMessage = Encoding.UTF8.GetBytes(messageToSend);

					queueChannel.BasicPublish(string.Empty, MainQueueName, null, body: encodedMessage);
					Console.WriteLine("message sent");
				}
			}
		}
	}
}
