using EventBus.Abstractions;
using EventBus.Events;
using IntegrationEventLogEF.Services;
using Message.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;

namespace GatewayRequestApi.Application.IntegrationEvents;

public class MessageIntegrationEventService : IMessageIntegrationEventService
{
    private readonly IEventBus _eventBus;
    private readonly MessageContext _messagingContext;
    private readonly Func<DbConnection, IIntegrationEventLogService> _integrationEventLogServiceFactory;
    private readonly IIntegrationEventLogService _eventLogService;
    private readonly ILogger<MessageIntegrationEventService> _logger;

    public MessageIntegrationEventService(IEventBus eventBus, MessageContext messagingContext, Func<DbConnection, IIntegrationEventLogService> integrationEventLogServiceFactory, ILogger<MessageIntegrationEventService> logger)
    {
        _eventBus = eventBus ?? throw new ArgumentException(nameof(eventBus));
        _messagingContext = messagingContext ?? throw new ArgumentException(nameof(messagingContext));
        _integrationEventLogServiceFactory = integrationEventLogServiceFactory ?? throw new ArgumentException(nameof(integrationEventLogServiceFactory));
        _eventLogService = _integrationEventLogServiceFactory(_messagingContext.Database.GetDbConnection());
        _logger = logger ?? throw new ArgumentException(nameof(logger));
    }

    public async Task PublishEventsThroughEventBusAsync(Guid transactionId)
    {
        var pendingLogEvents = await _eventLogService.RetrieveEventLogsPendingToPublishAsync(transactionId);

        foreach (var logEvt in pendingLogEvents)
        {
            _logger.LogInformation("Publishing integration event: {IntegrationEventId} - ({@IntegrationEvent})", logEvt.EventId, logEvt.IntegrationEvent);

            try
            {
                await _eventLogService.MarkEventAsInProgressAsync(logEvt.EventId);
                _eventBus.Publish(logEvt.IntegrationEvent);
                await _eventLogService.MarkEventAsPublishedAsync(logEvt.EventId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error publishing integration event: {IntegrationEventId}", logEvt.EventId);

                await _eventLogService.MarkEventAsFailedAsync(logEvt.EventId);
            }
        }
    }

    public async Task AddAndSaveEventAsync(IntegrationEvent evt)
    {
        _logger.LogInformation("Enqueuing integration event {IntegrationEventId} to repository ({@IntegrationEvent})", evt.Id, evt);

        await _eventLogService.SaveEventAsync(evt, _messagingContext.GetCurrentTransaction());
    }
}
