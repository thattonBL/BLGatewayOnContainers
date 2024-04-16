using GatewayGrpcService.Protos;
using GatewayGrpcService.Queries;
using Grpc.Core;


namespace GatewayGrpcService.Services
{
    public class RsiMessageService : Protos.GatewayGrpcService.GatewayGrpcServiceBase
    {
        private readonly IGatewayRequestQueries _gatewayRequestQueries;
        
        public RsiMessageService(IGatewayRequestQueries gatewayRequestQueries)
        {
            _gatewayRequestQueries = gatewayRequestQueries;
        }
        
        public override async Task GetGatewayMessages(RequestParams request, IServerStreamWriter<Protos.RSIMessage> responseStream, ServerCallContext contexts)
        {
            var messages = await _gatewayRequestQueries.GetRSIMEssagesFromDbAsync();

            foreach (var message in messages)
            {
                var responseMessage = new Protos.RSIMessage();
                responseMessage.Id = message.id;
                responseMessage.CollectionCode = message.collection_code;
                responseMessage.Shelfmark = message.shelfmark;
                responseMessage.VolumeNumber = message.volume_number;
                responseMessage.StorageLocationCode = message.storage_location_code;
                responseMessage.Author = message.author;
                responseMessage.Title = message.title;
                responseMessage.PublicationDate = Google.Protobuf.WellKnownTypes.Timestamp.FromDateTime(message.publication_date.ToUniversalTime());
                responseMessage.PeriodicalDate = Google.Protobuf.WellKnownTypes.Timestamp.FromDateTime(message.periodical_date.ToUniversalTime());
                responseMessage.ArticleLine1 = message.article_line1;
                responseMessage.ArticleLine2 = message.article_line2;
                responseMessage.CatalogueRecordUrl = message.catalogue_record_url;
                responseMessage.FurtherDetailsUrl = message.further_details_url;
                responseMessage.DtRequired = message.dt_required != null ? message.dt_required : "";
                responseMessage.Route = message.route;
                responseMessage.ReadingRoomStaffArea = message.reading_room_staff_area != null ? message.reading_room_staff_area : "";
                responseMessage.SeatNumber = message.seat_number;
                responseMessage.ReadingCategory = message.reading_category;
                responseMessage.Identifier = message.identifier;
                responseMessage.ReaderName = message.reader_name;
                responseMessage.ReaderType = message.reader_type;
                responseMessage.OperatorInformation = message.operator_information;
                responseMessage.ItemIdentity = message.item_identity;

                await responseStream.WriteAsync(responseMessage);
            }
        }
    }
}
