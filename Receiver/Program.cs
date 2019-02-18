using System;
using System.Text;
using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Utility;

namespace Receiver
{
	class Program
	{
		const string AppSettingsFile = "appsettings.json";
		const string HostNameKey = "HostName";
		const string MainQueueNameKey = "MainQueueName";
		static string HostName { get;set; }
		static string MainQueueName { get;set; }

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
			Console.WriteLine($"Starting {nameof(Receiver)} Console App");
			var queueFactory = new ConnectionFactory { HostName = HostName };

			using (var queueConnection = queueFactory.CreateConnection())
			using (var queueChannel = queueConnection.CreateModel())
			{
				queueChannel.QueueDeclare(MainQueueName, false, false, false, arguments: null);

				var consumer = new EventingBasicConsumer(queueChannel);
				consumer.Received += (model, ea) =>
				{
					var messageBody = ea.Body;
					var messageDecoded = Encoding.UTF8.GetString(messageBody);
					if (messageDecoded.ValidForStarWarsName())
						Console.WriteLine($"{DateTime.Now:MM/dd/yyyy hh:mm tt}: Hello {messageDecoded.StarWarsName()}, I am your father!");
				};

				queueChannel.BasicConsume(MainQueueName, true, consumer);
				Console.ReadLine();
			}
		}
	}
}