namespace vT.eCoffeeShop.Messaging.EventBus;

public interface IMessagesEventBus
{
    Task<bool> PublishAsync<T>(string queueName, T eventMessage);
    void Subscribe<T>(string queueName, Action<T> eventHandler);
}