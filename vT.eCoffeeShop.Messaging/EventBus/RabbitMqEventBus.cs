using System.Text;
using System.Text.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace vT.eCoffeeShop.Messaging.EventBus;

public class RabbitMqEventBus : IMessagesEventBus, IDisposable
{
    private readonly IModel _channel;
    private readonly IConnection _connection;
    private bool _disposed;

    public RabbitMqEventBus(string hostname = "localhost")
    {
        var factory = new ConnectionFactory { HostName = hostname };
        _connection = factory.CreateConnection();
        _channel = _connection.CreateModel();
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    public Task<bool> PublishAsync<T>(string queueName, T eventMessage)
    {
        if (_disposed) throw new ObjectDisposedException(nameof(RabbitMqEventBus));

        DeclareQueue(queueName);
        var body = SerializeMessage(eventMessage);

        try
        {
            _channel.BasicPublish(
                string.Empty,
                queueName,
                null,
                body
            );
            return Task.FromResult(true);
        }
        catch (Exception ex)
        {
            // Log exception (consider using a logging framework)
            Console.WriteLine($"Error publishing message: {ex.Message}");
            throw;
        }
    }

    public void Subscribe<T>(string queueName, Action<T> eventHandler)
    {
        if (_disposed) throw new ObjectDisposedException(nameof(RabbitMqEventBus));

        DeclareQueue(queueName);
        var consumer = new EventingBasicConsumer(_channel);

        consumer.Received += (_, ea) =>
        {
            try
            {
                var eventMessage = DeserializeMessage<T>(ea.Body);
                if (eventMessage != null) eventHandler(eventMessage);
            }
            catch (Exception ex)
            {
                // Log exception (consider using a logging framework)
                Console.WriteLine($"Error handling message: {ex.Message}");
            }
        };

        _channel.BasicConsume(queueName, true, consumer);
    }

    private void DeclareQueue(string queueName)
    {
        try
        {
            _channel.QueueDeclare(
                queueName,
                false,
                false,
                false,
                null
            );
        }
        catch (Exception ex)
        {
            // Log exception (consider using a logging framework)
            Console.WriteLine($"Error declaring queue: {ex.Message}");
            throw;
        }
    }

    private byte[] SerializeMessage<T>(T message)
    {
        try
        {
            var jsonString = JsonSerializer.Serialize(message);
            return Encoding.UTF8.GetBytes(jsonString);
        }
        catch (Exception ex)
        {
            // Log exception (consider using a logging framework)
            Console.WriteLine($"Error serializing message: {ex.Message}");
            throw;
        }
    }

    private T? DeserializeMessage<T>(ReadOnlyMemory<byte> body)
    {
        try
        {
            var messageString = Encoding.UTF8.GetString(body.Span);
            return JsonSerializer.Deserialize<T>(messageString);
        }
        catch (Exception ex)
        {
            // Log exception (consider using a logging framework)
            Console.WriteLine($"Error deserializing message: {ex.Message}");
            throw;
        }
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                _channel.Close();
                _connection.Close();
            }

            _disposed = true;
        }
    }
}