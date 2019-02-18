using System;
using System.Text;
using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;

namespace Sender
{
	class Program
	{
		const string AppSettingsFile = "appsettings.json";
		const string HostNameKey = "HostName";
		const string MainQueueNameKey = "MainQueueName";
		static string HostName { get; set; }
		static string MainQueueName { get; set; }

		static void Setup()
		{
			var config = new ConfigurationBuilder()
				.AddJsonFile(AppSettingsFile, true, true)
				.Build();
			HostName = config[HostNameKey];
			MainQueueName = config[MainQueueNameKey];
		}

		static void Main(string[] args)
		{
			Setup();

			Console.WriteLine($"Starting {nameof(Sender)} Console App");
			Console.WriteLine("Enter your greeting text, followed by your name.");
			Console.WriteLine("Eg. Hello my name is, Mark");

			var queueFactory = new ConnectionFactory { HostName = HostName };

			while (true)
			{
				using (var queueConnection = queueFactory.CreateConnection())
				using (var queueChannel = queueConnection.CreateModel())
				{
					queueChannel.QueueDeclare(MainQueueName, false, false, false, null);
					
					Console.Write("Type: ");
					var messageToSend = Console.ReadLine();
					var encodedMessage = Encoding.UTF8.GetBytes(messageToSend);

					queueChannel.BasicPublish(string.Empty, MainQueueName, null, body: encodedMessage);
					Console.WriteLine("Message sent");
				}
			}
		}
	}
}
