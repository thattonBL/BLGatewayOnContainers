using EventBus.Events;

namespace GatewayGrpcService.IntegrationEvents.Events;

public record RsiMessagePublishedIntegrationEvent : IntegrationEvent
{
    public static string EVENT_NAME = "RsiMessagePublished.IntegrationEvent";
    public RsiMessagePublishedIntegrationEvent(string rsiMessageId, string eventName, string appName)
    {
        RsiMessageId = rsiMessageId;
        EventName = eventName;
        AppName = appName;
    }
    public string RsiMessageId { get; init; }
    public string EventName { get; init; }
    public string AppName { get; init; }
}
