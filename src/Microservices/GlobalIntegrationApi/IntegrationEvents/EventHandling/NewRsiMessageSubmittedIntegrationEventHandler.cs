using EventBus.Abstractions;
using Events.Common.Events;
using GlobalIntegrationApi.IntegrationEvents.Events;
using IntegrationEventLogEF.Services;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;

namespace GlobalIntegrationApi.IntegrationEvents.EventHandling
{
    public class NewRsiMessageSubmittedIntegrationEventHandler : IIntegrationEventHandler<NewRsiMessageSubmittedIntegrationEvent>
    {
        private readonly ILogger<NewRsiMessageSubmittedIntegrationEventHandler> _logger;
        private readonly GlobalIntegrationContext _globalIntContext;
        private readonly Func<DbConnection, IIntegrationEventLogService> _integrationEventLogServiceFactory;
        private readonly IIntegrationEventLogService _eventLogService;

        public NewRsiMessageSubmittedIntegrationEventHandler(GlobalIntegrationContext context, Func<DbConnection, IIntegrationEventLogService> integrationEventLogServiceFactory, ILogger<NewRsiMessageSubmittedIntegrationEventHandler> logger)
        {
            _globalIntContext = context ?? throw new ArgumentException(nameof(context));
            _integrationEventLogServiceFactory = integrationEventLogServiceFactory ?? throw new ArgumentException(nameof(integrationEventLogServiceFactory));
            _eventLogService = _integrationEventLogServiceFactory(_globalIntContext.Database.GetDbConnection());
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task Handle(NewRsiMessageSubmittedIntegrationEvent @event)
        {
            //TODO Agg try catch around this also what role does IMediator play in the Context????
            Console.WriteLine($"New RSI GLOBAAAAALL !!!!!!!!  INtegration message submitted: {@event.RsiMessage.Identifier}");
            await using var transaction = await _globalIntContext.BeginTransactionAsync();
            {
                //TODO could loop through backed up messages here if required???
                await _eventLogService.SaveEventAsync(@event, _globalIntContext.GetCurrentTransaction());
                await _globalIntContext.CommitTransactionAsync(transaction);
            }
        }
    }
}
