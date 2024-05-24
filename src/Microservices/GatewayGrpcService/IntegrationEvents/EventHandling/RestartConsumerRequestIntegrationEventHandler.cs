using EventBus.Abstractions;
using GatewayGrpcService.IntegrationEvents.Events;
using GatewayGrpcService.Queries;
using GatewayGrpcService.Services;
using MediatR;

namespace GatewayGrpcService.IntegrationEvents.EventHandling;

public class RestartConsumerRequestIntegrationEventHandler : IIntegrationEventHandler<RestartConsumerRequestIntegrationEvent>
{
    private readonly IMessageServiceControl _messageServiceControl;
    private readonly ILogger _logger;
    private readonly IGatewayRequestQueries _gatewayRequestQueries;
    private readonly GrpcMessageService _grpcMessageService;

    public RestartConsumerRequestIntegrationEventHandler(GrpcMessageService grpcMessageService,  IGatewayRequestQueries gatewayRequestQueries, IMessageServiceControl messageServiceControl, ILogger<RestartConsumerRequestIntegrationEventHandler> logger)
{
        _messageServiceControl = messageServiceControl;
        _logger = logger;
        _gatewayRequestQueries = gatewayRequestQueries;
        _grpcMessageService = grpcMessageService;
    }
    public async Task Handle(RestartConsumerRequestIntegrationEvent @event)
    {
        var messages = await _gatewayRequestQueries.GetRSIMEssagesFromDbAsync();
        await _grpcMessageService.SendBulkRsiMessages(messages);
        //now delete or flag as sent
        var ackedMessages = await _gatewayRequestQueries.SetRsiMessagesToAckedAsync();
        _messageServiceControl.messageDeliveryPaused = false;
    }
}
