using EventBus.Events;

namespace GlobalIntegrationApi.IntegrationEvents.Events;

public record StopConsumerRequestIntegrationEvent : IntegrationEvent
{
    public static string EVENT_NAME = "StopConsumerRequest.IntegrationEvent";
    public StopConsumerRequestIntegrationEvent(string consumerId)
    {
        ConsumerId = consumerId;
        EventName = EVENT_NAME;
    }
    public string ConsumerId { get; init; }
    public string EventName { get; init; }
}
