using EventBus.Events;

namespace GlobalIntegrationApi.IntegrationEvents.Events;

public record RestartConsumerRequestIntegrationEvent : IntegrationEvent
{
    public static string EVENT_NAME = "RestartConsumerRequest.IntegrationEvent";
    public RestartConsumerRequestIntegrationEvent(string consumerId)
    {
        ConsumerId = consumerId;
        EventName = EVENT_NAME;
    }
    public string ConsumerId { get; init; }
    public string EventName { get; init; }
}
