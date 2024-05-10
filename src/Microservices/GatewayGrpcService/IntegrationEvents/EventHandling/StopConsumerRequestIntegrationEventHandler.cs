using EventBus.Abstractions;
using GatewayGrpcService.IntegrationEvents.Events;

namespace GatewayGrpcService.IntegrationEvents.EventHandling
{
    public class StopConsumerRequestIntegrationEventHandler : IIntegrationEventHandler<StopConsumerRequestIntegrationEvent>
    {
        private readonly ILogger<StopConsumerRequestIntegrationEventHandler> _logger;
        private readonly IEventBus _eventBus;
        
        public StopConsumerRequestIntegrationEventHandler(IEventBus eventBus, ILogger<StopConsumerRequestIntegrationEventHandler> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _eventBus = eventBus ?? throw new ArgumentNullException(nameof(eventBus));
        }
        public async Task Handle(StopConsumerRequestIntegrationEvent @event)
        {
            // add event to Redis Cache maybe?
            //dispatch a Domain Event that each Integration event picks up
            Console.WriteLine("We've DOne it! We havr requested that the processing of messages stops immediately!");
        }
    }
}
