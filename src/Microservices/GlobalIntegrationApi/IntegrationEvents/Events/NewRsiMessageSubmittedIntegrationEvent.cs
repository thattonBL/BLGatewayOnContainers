using EventBus.Events;
using System.Text.Json.Serialization;

namespace GlobalIntegrationApi.IntegrationEvents.Events;

public record NewRsiMessageSubmittedIntegrationEvent : IntegrationEvent
{
    public static string EVENT_NAME = "NewRsiMessageSubmitted.IntegrationEvent";

    public NewRsiMessageSubmittedIntegrationEvent(string rsiMessageId, string eventName)
    {
        RsiMessageId = rsiMessageId;
        EventName = eventName;
    }

    public string RsiMessageId { get; init; }

    public string EventName { get; init; }
}
