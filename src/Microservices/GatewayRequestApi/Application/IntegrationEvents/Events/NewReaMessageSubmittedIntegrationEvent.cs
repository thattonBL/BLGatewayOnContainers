using EventBus.Events;
using System.Text.Json.Serialization;

namespace GatewayRequestApi.Application.IntegrationEvents.Events;

public record NewReaMessageSubmittedIntegrationEvent : IntegrationEvent
{
    [JsonConstructor]
    public NewReaMessageSubmittedIntegrationEvent(string reaMessageId)
    {
        ReaMessageId = reaMessageId;
        eventName = "NewReaMessageSubmitted.IntegrationEvent";
    }

    [JsonInclude]
    public string ReaMessageId { get; init; }

    [JsonIgnore]
    public string eventName { get; init; }
}
