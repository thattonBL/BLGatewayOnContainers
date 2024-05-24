namespace GatewayGrpcService.Services;

public class MessageServiceControl : IMessageServiceControl
{
    public bool messageDeliveryPaused { get; set; }

    public MessageServiceControl()
    {
        messageDeliveryPaused = false;
    }
}
