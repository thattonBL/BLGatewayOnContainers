
using GatewayGrpcService.Protos;
using Grpc.Core;

public class GrpcMessageService : GatewayGrpcMessagingService.GatewayGrpcMessagingServiceBase
{
    public override Task<RSIRecieved> CreateStorageItemRequest(RSIMessage request, ServerCallContext context)
    {
        return Task.FromResult(new RSIRecieved { ItemIdentity = request.ItemIdentity });
    }
}
