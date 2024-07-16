using RabbitMQ.Client.Events;
using RabbitMqUtility;
using System.Text;

var setup = new RabbirmqSetup("RabbitConfiguration.json", "RabbitMqConfigs:factory")
    .CreateConnection();
var consumer = setup.GetConsumer();

consumer.Received += Consumer_Received;
var consumerTag = setup.BasicConsume("q1", false, consumer);
Console.WriteLine("Enter Key To Exit....");
Console.ReadKey();
void Consumer_Received(object? sender, BasicDeliverEventArgs e)
{
    string message = Encoding.UTF8.GetString(e.Body.ToArray());

    Console.WriteLine("message:"+message);
    setup.BasicNack(e.DeliveryTag, false, false);
}