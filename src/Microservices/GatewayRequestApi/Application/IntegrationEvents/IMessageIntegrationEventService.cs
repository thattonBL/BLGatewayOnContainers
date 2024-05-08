using EventBus.Events;

namespace GatewayRequestApi.Application.IntegrationEvents
{
    public interface IMessageIntegrationEventService
    {
        Task AddAndSaveEventAsync(IntegrationEvent evt);
        Task PublishEventsThroughEventBusAsync(Guid transactionId);
    }
}