using GatewayGrpcService.Data.Interfaces;

namespace GatewayGrpcService.Data.Repostories
{
    public interface IGatewayGrpcMessageRepo
    {
        IUnitOfWork UnitOfWork { get; }

        RSI Add(RSI rsiMessage);
        Common AddCommon(int RsiId);
    }
}