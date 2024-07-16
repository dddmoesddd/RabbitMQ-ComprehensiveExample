﻿using Producer;

var setup = new RabbitmqSetup("RabbitMqConfigs.json")
    .CreateConnection()
    .ExchangeDeclare("ex.fanout", "fanout", true, false, null)
    .QueueDeclare("q1", true, false, false, null)
    .QueueBind("q1", "ex1", "")
    .Publish("ex1", "", null, "messag1");
    


