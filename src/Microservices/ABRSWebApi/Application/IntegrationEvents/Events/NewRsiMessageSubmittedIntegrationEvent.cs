using EventBus.Events;
using System.Text.Json.Serialization;

namespace ABRSWebApi.Application.IntegrationEvents.Events;

public record NewRsiMessageSubmittedIntegrationEvent : IntegrationEvent
{   
    [JsonConstructor]
    public NewRsiMessageSubmittedIntegrationEvent(string rsiMessageId)
    {
        RsiMessageId = rsiMessageId;
    }

    [JsonInclude]
    public string RsiMessageId { get; init; }
}
