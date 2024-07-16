using Microsoft.Extensions.Configuration;
using Producer.Settings;
using RabbitMQ.Client;
using System.Text;

namespace Producer
{
    public class RabbitmqSetup
    {
        RabbitMqSettings _rabbitMqSettings;
        IConfigurationRoot _configurationRoot;
        IModel _channel;
        IConnection _connection;


        public RabbitmqSetup(string settingFile)
        {

            _configurationRoot = new ConfigurationBuilder()
.SetBasePath(Directory.GetCurrentDirectory())
// custom config file
.AddJsonFile(settingFile, optional: false, reloadOnChange: false)
.Build();

            _rabbitMqSettings = _configurationRoot.GetSection("RabbitMqConfigs:factory").Get<RabbitMqSettings>();

        }
        public RabbitmqSetup CreateConnection()
        {

            ConnectionFactory factory = new ConnectionFactory();
            factory.HostName = _rabbitMqSettings.HostName;
            factory.VirtualHost = _rabbitMqSettings.VirtualHost;
            factory.Port = int.Parse(_rabbitMqSettings.Port);
            factory.UserName = _rabbitMqSettings.UserName;
            factory.Password = _rabbitMqSettings.Password;

            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            return this;


        }

        public RabbitmqSetup ExchangeDeclare(string name, string type, bool durable, bool autoDelete, IDictionary<string, object> args)
        {
            _channel.ExchangeDeclare(name ,type, durable, autoDelete, args);
            return this;

        }

        public RabbitmqSetup QueueDeclare(string name, bool durable, bool exclusive, bool autodelete, IDictionary<string, object> args)
        {
            _channel.QueueDeclare(name, durable, exclusive, autodelete, args);
            return this;

        }

        public RabbitmqSetup QueueBind(string queue, string exchange, string routingKey)
        {
            _channel.QueueBind(queue, exchange, routingKey);
            return this;

        }
        public RabbitmqSetup Publish(string exchange, string routingKey, IBasicProperties basicProperties, string message)
        {
        _channel.BasicPublish(exchange, routingKey, basicProperties, Encoding.UTF8.GetBytes(message));
            return this;

        }
    }
}
