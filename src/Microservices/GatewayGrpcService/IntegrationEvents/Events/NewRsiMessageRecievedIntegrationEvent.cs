using EventBus.Events;

namespace GatewayGrpcService.IntegrationEvents.Events;

public record NewRsiMessageRecievedIntegrationEvent : IntegrationEvent
{
    public static string EVENT_NAME = "NewRsiMessageRecieved.IntegrationEvent";
    public NewRsiMessageRecievedIntegrationEvent(string rsiMessageId, string eventName, string appName)
    {
        RsiMessageId = rsiMessageId;
        EventName = eventName;
        AppName = appName;
    }
    public string RsiMessageId { get; init; }
    public string EventName { get; init; }
    public string AppName { get; init; }
}
