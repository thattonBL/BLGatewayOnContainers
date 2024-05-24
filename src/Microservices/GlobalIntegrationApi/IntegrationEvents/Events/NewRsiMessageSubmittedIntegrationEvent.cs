using Events.Common;
using Events.Common.Events;

namespace GlobalIntegrationApi.IntegrationEvents.Events;

public record NewRsiMessageSubmittedIntegrationEvent : BaseRsiMessageSubmittedIntegrationEvent
{
    public NewRsiMessageSubmittedIntegrationEvent(RsiPostItem rsiMessage, string eventName)
        : base(rsiMessage, eventName)
    {

    }
}
