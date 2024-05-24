using EventBus.Abstractions;
using GlobalIntegrationApi.IntegrationEvents.Events;
using IntegrationEventLogEF.Services;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;

namespace GlobalIntegrationApi.Services;

public class GlobalIntegrationServices : IGlobalIntegrationServices
{
    private readonly IEventBus _eventBus;
    private readonly Func<DbConnection, IIntegrationEventLogService> _integrationEventLogServiceFactory;
    private readonly IIntegrationEventLogService _eventLogService;
    private readonly GlobalIntegrationContext _globalIntContext;

    public GlobalIntegrationServices(GlobalIntegrationContext globalIntegrationContext, IEventBus eventBus, Func<DbConnection, IIntegrationEventLogService> integrationEventLogServiceFactory)
    {
        _eventBus = eventBus;
        _integrationEventLogServiceFactory = integrationEventLogServiceFactory;
        _globalIntContext = globalIntegrationContext;
        _eventLogService = _integrationEventLogServiceFactory(_globalIntContext.Database.GetDbConnection());
    }
    public async Task<bool> StopNamedCosumer(string consumerId)
    {
        var stopConsumerRequestIntegrationEvent = new StopConsumerRequestIntegrationEvent(consumerId);
        await using var transaction = await _globalIntContext.BeginTransactionAsync();
        {
            await _eventLogService.SaveEventAsync(stopConsumerRequestIntegrationEvent, _globalIntContext.GetCurrentTransaction());
            await _globalIntContext.CommitTransactionAsync(transaction);
        }      
        await Task.Run(() => _eventBus.Publish(stopConsumerRequestIntegrationEvent));
        return true;
    }

    public async Task<bool> RestartNamedCosumer(string consumerId)
    {
        var restartConsumerRequestIntegrationEvent = new RestartConsumerRequestIntegrationEvent(consumerId);
        await using var transaction = await _globalIntContext.BeginTransactionAsync();
        {
            await _eventLogService.SaveEventAsync(restartConsumerRequestIntegrationEvent, _globalIntContext.GetCurrentTransaction());
            await _globalIntContext.CommitTransactionAsync(transaction);
        }
        await Task.Run(() => _eventBus.Publish(restartConsumerRequestIntegrationEvent));
        return true;
    }
}
