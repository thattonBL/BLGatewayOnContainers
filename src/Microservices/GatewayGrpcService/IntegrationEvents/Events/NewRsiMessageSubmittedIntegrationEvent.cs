using EventBus.Events;

namespace GatewayGrpcService.IntegrationEvents.Events;

public record NewRsiMessageSubmittedIntegrationEvent : IntegrationEvent
{
    public NewRsiMessageSubmittedIntegrationEvent(string rsiMessageId)
    {
        RsiMessageId = rsiMessageId;
    }

    public string RsiMessageId { get; init; }
}
