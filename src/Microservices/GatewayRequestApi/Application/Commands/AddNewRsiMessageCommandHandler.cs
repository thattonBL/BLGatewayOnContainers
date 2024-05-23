using Events.Common.Events;
using GatewayRequestApi.Application.IntegrationEvents;
using GatewayRequestApi.Application.IntegrationEvents.Events;
using GatewayRequestApi.Models;
using Message.Domain.MessageAggregate;
using Message.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Globalization;

namespace GatewayRequestApi.Application.Commands;

//The Command Handler that implements the MediatR IRequestHandler interface
public class AddNewRsiMessageCommandHandler : IRequestHandler<AddNewRsiMessageCommand, bool>
{
    private readonly IMediator _mediator;
    private readonly IMessageIntegrationEventService _messageIntegrationEventService;
    private readonly IMessageRepository _messageRepository;
    private readonly ILogger<AddNewRsiMessageCommandHandler> _logger;
    public AddNewRsiMessageCommandHandler(IMediator mediator,
               IMessageIntegrationEventService messageIntegrationEventService,
                      IMessageRepository messageRepository,
                             ILogger<AddNewRsiMessageCommandHandler> logger)
    {
        _mediator = mediator ?? throw new ArgumentException(nameof(mediator));
        _messageIntegrationEventService = messageIntegrationEventService ?? throw new ArgumentException(nameof(messageIntegrationEventService));
        _messageRepository = messageRepository ?? throw new ArgumentException(nameof(messageRepository));
        _logger = logger ?? throw new ArgumentException(nameof(logger));
    }
    public async Task<bool> Handle(AddNewRsiMessageCommand request, CancellationToken cancellationToken)
    {
        //The data passed into the request to the Controller method
        var postedMsgData = request.Message;

        //Create a new integration message for the request and add it to the Integration Event table
        var newRsiMessageIntEvent = new NewRsiMessageSubmittedIntegrationEvent(postedMsgData, NewRsiMessageSubmittedIntegrationEvent.EVENT_NAME);
        await _messageIntegrationEventService.AddAndSaveEventAsync(newRsiMessageIntEvent);
        //We could use AutoFac for this but simpley copying properties across with some parsing
        var message = new RsiMessage(postedMsgData.CollectionCode,
            postedMsgData.Shelfmark, postedMsgData.VolumeNumber, postedMsgData.StorageLocationCode, 
                postedMsgData.Author, postedMsgData.Title, DateTime.ParseExact(postedMsgData.PublicationDate, "dd-MM-yyyy", CultureInfo.InvariantCulture), DateTime.ParseExact(postedMsgData.PeriodicalDate, "dd-MM-yyyy", CultureInfo.InvariantCulture), 
                    postedMsgData.ArticleLine1, postedMsgData.ArticleLine2, postedMsgData.CatalogueRecordUrl, postedMsgData.FurtherDetailsUrl, 
                        postedMsgData.DtRequired, postedMsgData.Route, postedMsgData.ReadingRoomStaffArea, postedMsgData.SeatNumber, postedMsgData.ReadingCategory, postedMsgData.Identifier,
                            postedMsgData.ReaderName, Int32.Parse(postedMsgData.ReaderType), postedMsgData.OperatorInformation, postedMsgData.ItemIdentity);

        //Add the new message to the repo
        _messageRepository.Add(message);
        //Bundles the database commit with the db changes that are the result of the dispatch of any domain events i.e. changes of RSI status. (which we don't currently have)
        await _messageRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        //The message has to be saved before we can get its auto generated ID
        _messageRepository.AddCommon(message.Id);
        //We just use the direct SaveChanges as we don't need to dispatch Domain Events this time.
        await _messageRepository.UnitOfWork.SaveChangesAsync(cancellationToken);    

        return true;
    }
}
