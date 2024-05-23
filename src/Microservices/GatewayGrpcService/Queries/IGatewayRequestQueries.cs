namespace GatewayGrpcService.Queries
{
    public interface IGatewayRequestQueries
    {
        Task<IEnumerable<RSIMessage>> GetRSIMEssagesFromDbAsync();
        Task<IEnumerable<Common>> SetRsiMessagesToAckedAsync();
    }
}