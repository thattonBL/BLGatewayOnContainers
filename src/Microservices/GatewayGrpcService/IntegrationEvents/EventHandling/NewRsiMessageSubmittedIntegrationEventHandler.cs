using EventBus.Abstractions;
using GatewayGrpcService.IntegrationEvents.Events;

namespace GatewayGrpcService.IntegrationEvents.EventHandling
{
    public class NewRsiMessageSubmittedIntegrationEventHandler : IIntegrationEventHandler<NewRsiMessageSubmittedIntegrationEvent>
    {
        private readonly ILogger<NewRsiMessageSubmittedIntegrationEventHandler> _logger;
        private readonly IEventBus _eventBus;
        
        public NewRsiMessageSubmittedIntegrationEventHandler(IEventBus eventBus, ILogger<NewRsiMessageSubmittedIntegrationEventHandler> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _eventBus = eventBus ?? throw new ArgumentNullException(nameof(eventBus));
        }

        public async Task Handle(NewRsiMessageSubmittedIntegrationEvent @event)
        {
            var newRsiMessageRecievedEvent = new NewRsiMessageRecievedIntegrationEvent(@event.RsiMessageId, NewRsiMessageRecievedIntegrationEvent.EVENT_NAME, "GatewayGrpcService");
            // add event to Redis Cache maybe?
            await Task.Run( () => _eventBus.Publish(newRsiMessageRecievedEvent));
            Console.WriteLine("We've DOne it! We've sent the message to the Global Integration API!");

        }
    }   
}
