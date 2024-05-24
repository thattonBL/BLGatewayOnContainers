using GatewayRequestApi.Application.IntegrationEvents;
using GatewayRequestApi.Application.IntegrationEvents.Events;
using GatewayRequestApi.Models;
using Message.Domain.MessageAggregate;
using Message.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Globalization;

namespace GatewayRequestApi.Application.Commands;

//The Command Handler that implements the MediatR IRequestHandler interface
public class AddNewReaMessageCommandHandler : IRequestHandler<AddNewReaMessageCommand, bool>
{
    private readonly IMediator _mediator;
    private readonly IMessageIntegrationEventService _messageIntegrationEventService;
    private readonly IMessageRepository _messageRepository;
    private readonly ILogger<AddNewReaMessageCommandHandler> _logger;
    public AddNewReaMessageCommandHandler(IMediator mediator,
               IMessageIntegrationEventService messageIntegrationEventService,
                      IMessageRepository messageRepository,
                             ILogger<AddNewReaMessageCommandHandler> logger)
    {
        _mediator = mediator ?? throw new ArgumentException(nameof(mediator));
        _messageIntegrationEventService = messageIntegrationEventService ?? throw new ArgumentException(nameof(messageIntegrationEventService));
        _messageRepository = messageRepository ?? throw new ArgumentException(nameof(messageRepository));
        _logger = logger ?? throw new ArgumentException(nameof(logger));
    }
    public async Task<bool> Handle(AddNewReaMessageCommand request, CancellationToken cancellationToken)
    {
        //The data passed into the request to the Controller method
        var postedMsgData = request.Message;

        //Create a new integration message for the request and add it to the Integration Event table
        var newReaMessageIntEvent = new NewReaMessageSubmittedIntegrationEvent(postedMsgData.Container_id);
        await _messageIntegrationEventService.AddAndSaveEventAsync(newReaMessageIntEvent);
        //We could use AutoFac for this but simpley copying properties across with some parsing
        var message = new ReaMessage(postedMsgData.Dt_of_action, postedMsgData.Request_response_flag,
                                            postedMsgData.Failure_code, Int32.Parse(postedMsgData.Container_id),
                                                postedMsgData.Text_message, postedMsgData.Stack_identity, postedMsgData.Tray_identity);

        //Add the new message to the repo
        _messageRepository.Add(message);
        //Bundles the database commit with the db changes that are the result of the dispatch of any domain events i.e. changes of RSI status. (which we don't currently have)
        await _messageRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);       

        return true;
    }
}
