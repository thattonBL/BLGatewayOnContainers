using ABRSWebApi.Models;
using Message.Domain.MessageAggregate;
using System.Globalization;

namespace ABRSWebApi.Application.Commands;

public class CreateRsiMessageCommandHandler : IRequestHandler<CreateRsiMessageCommand, RsiMessageDTO>
{
    public Task<RsiMessageDTO> Handle(CreateRsiMessageCommand request, CancellationToken cancellationToken)
    {
        var message = RsiMessage.NewDraft();
        return Task.FromResult(RsiMessageDTO.FromRsiPostItem(request.Message));
    }
}

public record RsiMessageDTO
{
    public string collectionCode { get; init; }
    public string shelfmark { get; init; }
    public string volumeNumber { get; init; }
    public string storageLocationCode { get; init; }
    public string author { get; init; }
    public string title { get; init; }
    public DateTime publicationDate { get; init; }
    public DateTime periodicalDate { get; init; }
    public string articleLine1 { get; init; }
    public string articleLine2 { get; init; }
    public string catalogueRecordUrl { get; init; }
    public string furtherDetailsUrl { get; init; }
    public string dtRequired { get; init; }
    public string route { get; init; }
    public string readingRoomStaffArea { get; init; }
    public string seatNumber { get; init; }
    public string readingCategory { get; init; }
    public string identifier { get; init; }
    public string readerName { get; init; }
    public int readerType { get; init; }
    public string operatorInformation { get; init; }
    public string itemIdentity { get; init; }

    public static RsiMessageDTO FromRsiPostItem(RsiPostItem message)
    {
        return new RsiMessageDTO
        {
            collectionCode = message.CollectionCode,
            shelfmark = message.Shelfmark,
            volumeNumber = message.VolumeNumber,
            storageLocationCode = message.StorageLocationCode,
            author = message.Author,
            title = message.Title,
            publicationDate = DateTime.ParseExact(message.PublicationDate,"dd-MM-yyyy", CultureInfo.InvariantCulture),
            periodicalDate = DateTime.ParseExact(message.PeriodicalDate,"dd-MM-yyyy", CultureInfo.InvariantCulture),
            articleLine1 = message.ArticleLine1,
            articleLine2 = message.ArticleLine2,
            catalogueRecordUrl = message.CatalogueRecordUrl,
            furtherDetailsUrl = message.FurtherDetailsUrl,
            dtRequired = message.DtRequired,
            route = message.Route,
            readingRoomStaffArea = message.ReadingRoomStaffArea,
            seatNumber = message.SeatNumber,
            readingCategory = message.ReadingCategory,
            identifier = message.Identifier,
            readerName = message.ReaderName,
            readerType = Int32.Parse(message.ReaderType),
            operatorInformation = message.OperatorInformation,
            itemIdentity = message.ItemIdentity
        };
    }
}