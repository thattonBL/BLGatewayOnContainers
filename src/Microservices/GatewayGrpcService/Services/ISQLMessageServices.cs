using GatewayGrpcService.Data;

namespace GatewayGrpcService.Services
{
    public interface ISQLMessageServices
    {
        Task AddNewRsiMessage(RSI message);
    }
}