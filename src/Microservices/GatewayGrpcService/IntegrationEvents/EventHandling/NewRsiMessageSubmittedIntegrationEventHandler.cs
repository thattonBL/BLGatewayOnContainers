using EventBus.Abstractions;
using Events.Common.Events;
using GatewayGrpcService.Data;
using GatewayGrpcService.IntegrationEvents.Events;
using GatewayGrpcService.Services;
using System.Globalization;

namespace GatewayGrpcService.IntegrationEvents.EventHandling
{
    public class NewRsiMessageSubmittedIntegrationEventHandler : IIntegrationEventHandler<NewRsiMessageSubmittedIntegrationEvent>
    {
        private readonly ILogger<NewRsiMessageSubmittedIntegrationEventHandler> _logger;
        private readonly IEventBus _eventBus;
        private readonly ISQLMessageServices _sqlMessageServices;
        private readonly GrpcMessageService _grpcMessageService;
        private readonly IMessageServiceControl _messageServiceControl;
        
        public NewRsiMessageSubmittedIntegrationEventHandler(GrpcMessageService grpcMessageService, ISQLMessageServices sqlMessageService, IMessageServiceControl messageServiceControl, IEventBus eventBus, ILogger<NewRsiMessageSubmittedIntegrationEventHandler> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _eventBus = eventBus ?? throw new ArgumentNullException(nameof(eventBus));
            _sqlMessageServices = sqlMessageService ?? throw new ArgumentNullException(nameof(sqlMessageService));
            _grpcMessageService = grpcMessageService ?? throw new ArgumentNullException(nameof(grpcMessageService));
            _messageServiceControl = messageServiceControl ?? throw new ArgumentNullException(nameof(messageServiceControl));
        }

        //TODO: We need to wrap this in a Behaviour as per the CQRS pattern in the GatewyRequestAPI
        public async Task Handle(NewRsiMessageSubmittedIntegrationEvent @event)
        {
            var newRsiMessageRecievedEvent = new NewRsiMessageRecievedIntegrationEvent(@event.RsiMessage.Identifier, NewRsiMessageRecievedIntegrationEvent.EVENT_NAME, "GatewayGrpcService");
            // add event to Redis Cache maybe?
            await Task.Run( () => _eventBus.Publish(newRsiMessageRecievedEvent));
            //Console.WriteLine("We've DOne it! We've sent the message to the Global Integration API!");
            var rawRsi = @event.RsiMessage;
            //check to see if the servuce is paused
            if (!_messageServiceControl.messageDeliveryPaused)
            {
                //if it isn't then send the message using the data from the event
            await _grpcMessageService.SendSingleRsiMessage(@event.RsiMessage);
            }
            else
            {
                var rsiPoco = new RSI(rawRsi.CollectionCode, rawRsi.Shelfmark, rawRsi.VolumeNumber, rawRsi.StorageLocationCode, rawRsi.Author,
                                        rawRsi.Title, DateTime.ParseExact(rawRsi.PublicationDate, "dd-MM-yyyy", CultureInfo.InvariantCulture),
                                            DateTime.ParseExact(rawRsi.PeriodicalDate, "dd-MM-yyyy", CultureInfo.InvariantCulture), rawRsi.ArticleLine1, rawRsi.ArticleLine2, rawRsi.CatalogueRecordUrl, rawRsi.FurtherDetailsUrl,
                                                rawRsi.DtRequired, rawRsi.Route, rawRsi.ReadingRoomStaffArea, rawRsi.SeatNumber, rawRsi.ReadingCategory, rawRsi.Identifier,
                                                    rawRsi.ReaderName, Int32.Parse(rawRsi.ReaderType), rawRsi.OperatorInformation, rawRsi.ItemIdentity);

                await _sqlMessageServices.AddNewRsiMessage(rsiPoco);
            }
        }
    }   
}
