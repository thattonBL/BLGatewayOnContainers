namespace Events.Common;

public record RsiPostItem
{
    public string CollectionCode { get; set; }
    public string Shelfmark { get; set; }
    public string VolumeNumber { get; set; }
    public string StorageLocationCode { get; set; }
    public string Author { get; set; }
    public string Title { get; set; }
    public string PublicationDate { get; set; }
    public string PeriodicalDate { get; set; }
    public string ArticleLine1 { get; set; }
    public string ArticleLine2 { get; set; }
    public string CatalogueRecordUrl { get; set; }
    public string FurtherDetailsUrl { get; set; }
    public string DtRequired { get; set; }
    public string Route { get; set; }
    public string ReadingRoomStaffArea { get; set; }
    public string SeatNumber { get; set; }
    public string ReadingCategory { get; set; }
    public string Identifier { get; set; }
    public string ReaderName { get; set; }
    public string ReaderType { get; set; }
    public string OperatorInformation { get; set; }
    public string ItemIdentity { get; set; }
}
