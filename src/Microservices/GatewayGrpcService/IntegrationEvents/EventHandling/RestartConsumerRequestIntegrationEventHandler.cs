using EventBus.Abstractions;
using GatewayGrpcService.IntegrationEvents.Events;
using GatewayGrpcService.Models;
using GatewayGrpcService.Queries;
using GatewayGrpcService.Services;
using MediatR;
using Microsoft.Extensions.Logging;

namespace GatewayGrpcService.IntegrationEvents.EventHandling;

public class RestartConsumerRequestIntegrationEventHandler : IIntegrationEventHandler<RestartConsumerRequestIntegrationEvent>
{
    private readonly IMessageServiceControl _messageServiceControl;
    private readonly IEventBus _eventBus;
    private readonly ILogger _logger;
    private readonly IGatewayRequestQueries _gatewayRequestQueries;
    private readonly GrpcMessageService _grpcMessageService;

    public RestartConsumerRequestIntegrationEventHandler(GrpcMessageService grpcMessageService,  IGatewayRequestQueries gatewayRequestQueries, IMessageServiceControl messageServiceControl, IEventBus eventBus, ILogger<RestartConsumerRequestIntegrationEventHandler> logger)
{
        _messageServiceControl = messageServiceControl;
        _eventBus = eventBus;
        _logger = logger;
        _gatewayRequestQueries = gatewayRequestQueries;
        _grpcMessageService = grpcMessageService;
    }
    public async Task Handle(RestartConsumerRequestIntegrationEvent @event)
    {
        var messages = await _gatewayRequestQueries.GetRSIMEssagesFromDbAsync();
        var responseList = await _grpcMessageService.SendBulkRsiMessages(messages);
        //aynsc loop to publish event for every item
        await LoopAsync(responseList);
        //now delete or flag as sent
        var ackedMessages = await _gatewayRequestQueries.SetRsiMessagesToAckedAsync();
        _messageServiceControl.messageDeliveryPaused = false;
    }

    public Task DispatchPublishedEvent(RsiMessageRecievedDataModel item)
    {
        var newRsiPublishedEvent = new RsiMessagePublishedIntegrationEvent(item.ItemIdentity, RsiMessagePublishedIntegrationEvent.EVENT_NAME, "GatewayGrpcService");
        _eventBus.Publish(newRsiPublishedEvent);
        return Task.CompletedTask;
    }

    public async Task LoopAsync(IEnumerable<RsiMessageRecievedDataModel> sentMessages)
    {
        List<Task> listOfTasks = new List<Task>();

        foreach (var message in sentMessages)
        {
            listOfTasks.Add(DispatchPublishedEvent(message));
        }

        await Task.WhenAll(listOfTasks);
    }
}
