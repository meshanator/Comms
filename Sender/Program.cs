using System;
using System.Text;
using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;

namespace Sender
{
	class Program
	{
		public const string AppSettingsFile = "appsettings.json";
		public const string HostNameKey = "HostName";
		public const string MainQueueNameKey = "MainQueueName";

		static void Main(string[] args)
		{
			Console.WriteLine($"Starting {nameof(Sender)} Console App");

			var config = new ConfigurationBuilder()
				.AddJsonFile(AppSettingsFile, true, true)
				.Build();
			var hostName = config[HostNameKey];
			var mainQueueName = config[MainQueueNameKey];

			var queueFactory = new ConnectionFactory { HostName = hostName };
			while (true)
			{
				using (var queueConnection = queueFactory.CreateConnection())
				using (var queueChannel = queueConnection.CreateModel())
				{
					queueChannel.QueueDeclare(mainQueueName, false, false, false, null);

					Console.Write("Enter a name: ");
					var messageToSend = Console.ReadLine();
					var encodedMessage = Encoding.UTF8.GetBytes(messageToSend);

					queueChannel.BasicPublish(string.Empty, mainQueueName, null, body: encodedMessage);
					Console.WriteLine("message sent");
				}
			}
		}
	}
}
