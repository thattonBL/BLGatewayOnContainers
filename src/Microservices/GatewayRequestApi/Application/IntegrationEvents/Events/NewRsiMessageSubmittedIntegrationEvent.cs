using EventBus.Events;
using System.Text.Json.Serialization;

namespace GatewayRequestApi.Application.IntegrationEvents.Events;

public record NewRsiMessageSubmittedIntegrationEvent : IntegrationEvent
{
    public static string EVENT_NAME = "NewRsiMessageSubmitted.IntegrationEvent";

    [JsonConstructor]
    public NewRsiMessageSubmittedIntegrationEvent(string rsiMessageId, string eventName)
    {
        RsiMessageId = rsiMessageId;
        EventName = eventName;
    }

    [JsonInclude]
    public string RsiMessageId { get; init; }

    [JsonInclude]
    public string EventName { get; init; }
}
