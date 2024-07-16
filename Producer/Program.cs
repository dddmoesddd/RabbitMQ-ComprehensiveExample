
//fanout
using RabbitMqUtility;

var setup = new RabbirmqSetup("RabbitConfiguration.json", "RabbitMqConfigs:factory")
    .CreateConnection()
    .ExchangeDeclare("ex1", "fanout", true, false, null)
    .QueueDeclare("q1", true, false, false, null)
    .QueueBind("q1", "ex1", "")
    .Publish("ex1", "", null, "messag1");

Console.WriteLine("Enter Key To Exit....");
Console.ReadKey();


setup.QueueDelete("q1").ExchangeDelete("ex1");
setup.ChannelClose().ConnectionClose();

//docker run --rm -it --hostname my-rabbit -p 15672:15672 - p 5672:5672 rabbitmq: 3 - management