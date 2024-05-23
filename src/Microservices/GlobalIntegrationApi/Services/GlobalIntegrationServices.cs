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

    public async Task<bool> RestartNamedCosumer(string consumerId)
    {
        var restartConsumerRequestIntegrationEvent = new RestartConsumerRequestIntegrationEvent(consumerId);
        await Task.Run(() => _eventBus.Publish(restartConsumerRequestIntegrationEvent));
        return true;
    }
}
