using Microsoft.EntityFrameworkCore.Metadata;
using RabbitMQ.Client;
using System.Text;

public class RabbitMqPublisher
{
    private readonly IConnection _connection;
    private readonly IModel _channel;

    public RabbitMqPublisher()
    {
        var factory = new ConnectionFactory()
        {
            HostName = "host.docker.internal", // Use "localhost" if running locally, but "host.docker.internal" from Docker
            UserName = "guest",
            Password = "guest",
            Port = 5672
        };

        //_connection = factory.CreateConnection();
        //_channel = _connection.CreateModel();

        //_channel.QueueDeclare(queue: "book-queue",
        //                     durable: false,
        //                     exclusive: false,
        //                     autoDelete: false,
        //                     arguments: null);
    }

    //public void Publish(string message)
    //{
    //    var body = Encoding.UTF8.GetBytes(message);

    //    _channel.BasicPublish(exchange: "",
    //                         routingKey: "book-queue",
    //                         basicProperties: null,
    //                         body: body);
    //}

    //public void Dispose()
    //{
    //    _channel.Close();
    //    _connection.Close();
    //}
}

