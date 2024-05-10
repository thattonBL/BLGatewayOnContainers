using EventBus.Abstractions;
using GlobalIntegrationApi.IntegrationEvents.Events;
using IntegrationEventLogEF.Services;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;

namespace GlobalIntegrationApi.IntegrationEvents.EventHandling;

public class NewRsiMessageRecievedIntegrationEventHandler : IIntegrationEventHandler<NewRsiMessageRecievedIntegrationEvent>
{
    private readonly ILogger<NewRsiMessageRecievedIntegrationEventHandler> _logger;
    private readonly GlobalIntegrationContext _globalIntContext;
    private readonly Func<DbConnection, IIntegrationEventLogService> _integrationEventLogServiceFactory;
    private readonly IIntegrationEventLogService _eventLogService;

    public NewRsiMessageRecievedIntegrationEventHandler(GlobalIntegrationContext context, Func<DbConnection, IIntegrationEventLogService> integrationEventLogServiceFactory, ILogger<NewRsiMessageRecievedIntegrationEventHandler> logger)
    {
        _globalIntContext = context ?? throw new ArgumentException(nameof(context));
        _integrationEventLogServiceFactory = integrationEventLogServiceFactory ?? throw new ArgumentException(nameof(integrationEventLogServiceFactory));
        _eventLogService = _integrationEventLogServiceFactory(_globalIntContext.Database.GetDbConnection());
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task Handle(NewRsiMessageRecievedIntegrationEvent @event)
    {
        //TODO Agg try catch around this also what role does IMediator play in the Context????
        Console.WriteLine($"New RSI RECIEVED ------  GLOBAAAAALL !!!!!!!!  INtegration message submitted: {@event.RsiMessageId}");
        await using var transaction = await _globalIntContext.BeginTransactionAsync();
        {
            //TODO could loop through backed up messages here if required???
            await _eventLogService.SaveEventAsync(@event, _globalIntContext.GetCurrentTransaction());
            await _globalIntContext.CommitTransactionAsync(transaction);
        }
    }
}
