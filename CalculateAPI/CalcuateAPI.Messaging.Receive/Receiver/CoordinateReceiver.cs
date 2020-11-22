using CalcuateAPI.Messaging.Receive.Options;
using CalculateAPI.Service.Models;
using CalculateAPI.Service.Services;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Globalization;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CalcuateAPI.Messaging.Receive.Receiver
{
    public class CoordinateReceiver : BackgroundService
    {
        private IModel _channel;
        private IConnection _connection;
        private readonly ICoordinateService _coordinateService;
        private readonly string _hostname;
        private readonly string _queueName;
        private readonly string _username;
        private readonly string _password;

        public CoordinateReceiver(ICoordinateService coordinateService, IOptions<RabbitMqConfiguration> rabbitMqOptions)
        {
            _hostname = rabbitMqOptions.Value.Hostname;
            _queueName = rabbitMqOptions.Value.QueueName;
            _username = rabbitMqOptions.Value.UserName;
            _password = rabbitMqOptions.Value.Password;
            _coordinateService = coordinateService;
            InitializeRabbitMqListener();
        }

        private void InitializeRabbitMqListener()
        {
            var factory = new ConnectionFactory
            {
                HostName = _hostname,
                UserName = _username,
                Password = _password
            };

            _connection = factory.CreateConnection();
            _connection.ConnectionShutdown += RabbitMQ_ConnectionShutdown;
            _channel = _connection.CreateModel();
            _channel.QueueDeclare(queue: _queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();

            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (ch, ea) =>
            {
                var content = Encoding.UTF8.GetString(ea.Body.ToArray());
                var coordinate = JsonConvert.DeserializeObject<Coordinate>(content);

                HandleMessage(coordinate);

                _channel.BasicAck(ea.DeliveryTag, false);
            };
            consumer.Shutdown += OnConsumerShutdown;
            consumer.Registered += OnConsumerRegistered;
            consumer.Unregistered += OnConsumerUnregistered;
            consumer.ConsumerCancelled += OnConsumerConsumerCancelled;

            _channel.BasicConsume(_queueName, false, consumer);

            return Task.CompletedTask;
        }

        private void HandleMessage(Coordinate coordinate)
        {
            var distance = _coordinateService.CalculateDistance(coordinate);
            WriteToOutput(coordinate, distance);
        }

        private void WriteToOutput(Coordinate coordinate, double distance)
        {
            string fileName = @"Output.txt";
            string destPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName);

            if (!File.Exists(destPath))
            {
                // Create a file to write to.
                using (StreamWriter sw = File.CreateText(destPath))
                {
                    sw.WriteLineAsync("source_latitude \t source_longitude \t destination_latitude \t destination_longitude \t distance(KM)");
                    sw.WriteLineAsync($"{coordinate.Source_Latitude} \t {coordinate.Source_Longitude} \t {coordinate.Destination_Latitude} \t {coordinate.Destination_Longitude} \t {distance.ToGBString()}");
                    return;
                }
            }

            using (StreamWriter sw = File.AppendText(destPath))
            {
                sw.WriteLineAsync($"{coordinate.Source_Latitude} \t {coordinate.Source_Longitude} \t {coordinate.Destination_Latitude} \t {coordinate.Destination_Longitude} \t {distance.ToGBString()}");
            }
        }

        private void OnConsumerConsumerCancelled(object sender, ConsumerEventArgs e)
        {
            throw new NotSupportedException();
        }

        private void OnConsumerUnregistered(object sender, ConsumerEventArgs e)
        {
            throw new NotSupportedException();
        }

        private void OnConsumerRegistered(object sender, ConsumerEventArgs e)
        {
            throw new NotSupportedException();
        }

        private void OnConsumerShutdown(object sender, ShutdownEventArgs e)
        {
            throw new NotSupportedException();
        }

        private void RabbitMQ_ConnectionShutdown(object sender, ShutdownEventArgs e)
        {
            throw new NotSupportedException();
        }

        public override void Dispose()
        {
            _channel.Close();
            _connection.Close();
            base.Dispose();
        }
    }

    public static class DoubleExtensions
    {
        public static string ToGBString(this double value)
        {
            return value.ToString(CultureInfo.GetCultureInfo("en-GB"));
        }
    }
}
