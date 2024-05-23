using GatewayGrpcService.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GatewayGrpcService.Data.Repostories;

public class GatewayGrpcMessageRepo : IGatewayGrpcMessageRepo
{
    private readonly GatewayGrpcContext _context;
    private readonly ILogger<GatewayGrpcMessageRepo> _logger;

    public IUnitOfWork UnitOfWork => _context;

    public GatewayGrpcMessageRepo(GatewayGrpcContext context, ILogger<GatewayGrpcMessageRepo> logger)
    {
        _context = context;
        _logger = logger;
    }

    public RSI Add(RSI rsiMessage)
    {
        return _context.RSIs.Add(rsiMessage).Entity;
    }

    public Common AddCommon(int RsiId)
    {
        var newCommon = new Common { dt_created = DateTime.Now, msg_target = RsiId, type = 1, is_acknowledged = false };
        return _context.Commons.Add(newCommon).Entity;
    }
}
