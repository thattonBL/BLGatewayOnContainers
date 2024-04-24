using EventBus.Events;

namespace ABRSWebApi.Application.IntegrationEvents
{
    public interface IMessageIntegrationEventService
    {
        Task AddAndSaveEventAsync(IntegrationEvent evt);
        Task PublishEventsThroughEventBusAsync(Guid transactionId);
    }
}