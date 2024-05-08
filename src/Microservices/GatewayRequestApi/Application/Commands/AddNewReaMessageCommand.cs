using GatewayRequestApi.Models;

namespace GatewayRequestApi.Application.Commands;

// The Command that implements the MediatR IRequest interface
public class AddNewReaMessageCommand : IRequest<bool>
{
    public ReaPostItem Message { get; private set; }

    public AddNewReaMessageCommand(ReaPostItem message)
    {
        Message = message;
    }
}
