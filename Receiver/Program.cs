using System;
using System.Text;
using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Receiver
{
	class Program
	{
		public const string AppSettingsFile = "appsettings.json";
		public const string HostNameKey = "HostName";
		public const string MainQueueNameKey = "MainQueueName";

		static void Main(string[] args)
		{
			Console.WriteLine($"Starting {nameof(Receiver)} Console App");

			var config = new ConfigurationBuilder()
				.AddJsonFile(AppSettingsFile, true, true)
				.Build();
			var hostName = config[HostNameKey];
			var mainQueueName = config[MainQueueNameKey];

			var queueFactory = new ConnectionFactory { HostName = hostName };

			using (var queueConnection = queueFactory.CreateConnection())
			using (var queueChannel = queueConnection.CreateModel())
			{
				queueChannel.QueueDeclare(mainQueueName, false, false, false, arguments: null);

				var consumer = new EventingBasicConsumer(queueChannel);
				consumer.Received += (model, ea) =>
				{
					var messageBody = ea.Body;
					var messageDecoded = Encoding.UTF8.GetString(messageBody);
					Console.WriteLine($"{DateTime.Now:MM/dd/yyyy hh:mm tt}: Hello {messageDecoded}, I am your father!");
				};

				queueChannel.BasicConsume(mainQueueName, true, consumer);
				Console.ReadLine();
			}
		}
	}
}
