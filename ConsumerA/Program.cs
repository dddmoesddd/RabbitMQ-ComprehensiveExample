using RabbitMqUtility;

var setup = new RabbirmqSetup("RabbitConfiguration.json", "RabbitMqConfigs:factory")
    .CreateConnection();
