using EventBus.Abstractions;
using GatewayGrpcService.IntegrationEvents.Events;
using GatewayGrpcService.Services;
using MediatR;

namespace GatewayGrpcService.IntegrationEvents.EventHandling
{
    public class StopConsumerRequestIntegrationEventHandler : IIntegrationEventHandler<StopConsumerRequestIntegrationEvent>
    {
        private readonly ILogger<StopConsumerRequestIntegrationEventHandler> _logger;
        private readonly IMessageServiceControl _messageServiceControl;
        
        public StopConsumerRequestIntegrationEventHandler(IMessageServiceControl messageServiceControl, ILogger<StopConsumerRequestIntegrationEventHandler> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _messageServiceControl = messageServiceControl;
        }
        public async Task Handle(StopConsumerRequestIntegrationEvent @event)
        {
            // add event to Redis Cache maybe?
            //dispatch a Domain Event that each Integration event picks up
            _logger.LogInformation("StopConsumerRequestIntegrationEvent recieved stopping sending message from GatewayGrpcService");
            _messageServiceControl.messageDeliveryPaused = true;
        }
    }
}
