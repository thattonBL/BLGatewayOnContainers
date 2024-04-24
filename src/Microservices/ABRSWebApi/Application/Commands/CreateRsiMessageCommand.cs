using ABRSWebApi.Models;

namespace ABRSWebApi.Application.Commands;

public class CreateRsiMessageCommand : IRequest<RsiMessageDTO>
{
    public RsiPostItem Message { get; private set; }

    public CreateRsiMessageCommand(RsiPostItem message)
    {
        Message = message;
    }
}
