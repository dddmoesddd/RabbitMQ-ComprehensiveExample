
using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using RabbitMqUtility.Settings;
using System.Text;

namespace RabbitMqUtility
{
    public class RabbirmqSetup
    {
        RabbitMqSettings _rabbitMqSettings;
        IConfigurationRoot _configurationRoot;
        IModel _channel;
        IConnection _connection;


        public RabbirmqSetup(string settingFile,string section)
        {

            _configurationRoot = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
            // custom config file
            .AddJsonFile(settingFile, optional: false, reloadOnChange: false)
            .Build();

            _rabbitMqSettings = _configurationRoot.GetSection(section).Get<RabbitMqSettings>();

        }
        public RabbirmqSetup CreateConnection()
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

        public RabbirmqSetup ExchangeDeclare(string name, string type, bool durable, bool autoDelete, IDictionary<string, object> args)
        {
            _channel.ExchangeDeclare(name, type, durable, autoDelete, args);
            return this;

        }

        public RabbirmqSetup QueueDeclare(string name, bool durable, bool exclusive, bool autodelete, IDictionary<string, object> args)
        {
            _channel.QueueDeclare(name, durable, exclusive, autodelete, args);
            return this;

        }

        public RabbirmqSetup QueueBind(string queue, string exchange, string routingKey)
        {
            _channel.QueueBind(queue, exchange, routingKey);
            return this;

        }
        public RabbirmqSetup Publish(string exchange, string routingKey, IBasicProperties basicProperties, string message)
        {
            _channel.BasicPublish(exchange, routingKey, basicProperties, Encoding.UTF8.GetBytes(message));
            return this;

        }

        public  RabbirmqSetup QueueDelete(string queueName)
        {

            _channel.QueueDelete(queueName);
            return this;
        }

        public RabbirmqSetup ExchangeDelete(string exchangeName)
        {

            _channel.ExchangeDelete(exchangeName);
            return this;
        }

        public RabbirmqSetup ChannelClose()
        {

            _channel.Close();
            return this;
        }

        public RabbirmqSetup  ConnectionClose()
        {

            _connection.Close();
            return this;
        }
    }
}
