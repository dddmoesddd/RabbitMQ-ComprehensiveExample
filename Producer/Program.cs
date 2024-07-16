using Producer;
//fanout
var setup = new RabbitmqProducer("RabbitConfiguration.json")
    .CreateConnection()
    .ExchangeDeclare("ex1", "fanout", true, false, null)
    .QueueDeclare("q1", true, false, false, null)
    .QueueBind("q1", "ex1", "")
    .Publish("ex1", "", null, "messag1");
    



