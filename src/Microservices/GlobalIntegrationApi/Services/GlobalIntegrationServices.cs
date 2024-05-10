using EventBus.Abstractions;
using GlobalIntegrationApi.IntegrationEvents.Events;

namespace GlobalIntegrationApi.Services;

public class GlobalIntegrationServices : IGlobalIntegrationServices
{
    private readonly IEventBus _eventBus;

    public GlobalIntegrationServices(IEventBus eventBus)
    {
        _eventBus = eventBus;
    }
    public async Task<bool> StopNamedCosumer(string consumerId)
    {
        var stopConsumerRequestIntegrationEvent = new StopConsumerRequestIntegrationEvent(consumerId);
        await Task.Run(() => _eventBus.Publish(stopConsumerRequestIntegrationEvent));
        return true;
    }
}
