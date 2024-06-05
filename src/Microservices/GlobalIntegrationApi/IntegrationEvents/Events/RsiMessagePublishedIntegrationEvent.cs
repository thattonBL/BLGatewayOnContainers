using EventBus.Events;

namespace GlobalIntegrationApi.IntegrationEvents.Events;

public record RsiMessagePublishedIntegrationEvent : IntegrationEvent
{
    public static string EVENT_NAME = "RsiMessagePublished.IntegrationEvent";
    public RsiMessagePublishedIntegrationEvent(string rsiMessageId, string eventName)
    {
        RsiMessageId = rsiMessageId;
        EventName = eventName;
    }
    public string RsiMessageId { get; init; }
    public string EventName { get; init; }
}
