using EventBus.Abstractions;
using GlobalIntegrationApi.IntegrationEvents.Events;

namespace GlobalIntegrationApi.IntegrationEvents.EventHandling
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
            Console.WriteLine($"New RSI GLOBAAAAALL !!!!!!!!  INtegration message submitted: {@event.RsiMessageId}");
            throw new System.NotImplementedException();
        }
    }
}
