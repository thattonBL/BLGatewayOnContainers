using GatewayRequestApi.Models;

namespace GatewayRequestApi.Application.Commands;

// The Command that implements the MediatR IRequest interface
public class AddNewRsiMessageCommand : IRequest<bool>
{
    public RsiPostItem Message { get; private set; }

    public AddNewRsiMessageCommand(RsiPostItem message)
    {
        Message = message;
    }
}
