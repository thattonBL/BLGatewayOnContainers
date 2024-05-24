namespace GatewayGrpcService.Services
{
    public interface IMessageServiceControl
    {
        bool messageDeliveryPaused { get; set; }
    }
}