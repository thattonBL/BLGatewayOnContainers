using EventBus.Abstractions;
using GatewayGrpcService.IntegrationEvents.Events;

namespace GatewayGrpcService.IntegrationEvents.EventHandling
{
    public class NewRsiMessageSubmittedIntegrationEventHandler : IIntegrationEventHandler<NewRsiMessageSubmittedIntegrationEvent>
    {
        private readonly ILogger<NewRsiMessageSubmittedIntegrationEventHandler> _logger;
        
        public NewRsiMessageSubmittedIntegrationEventHandler(ILogger<NewRsiMessageSubmittedIntegrationEventHandler> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task Handle(NewRsiMessageSubmittedIntegrationEvent @event)
        {
            _logger.LogDebug("New RSI message submitted: {RsiMessageId}", @event.RsiMessageId);
            Console.WriteLine($"New RSI message submitted: {@event.RsiMessageId}");
            throw new System.NotImplementedException();
        }
    }   
}
