using GatewayGrpcService.Data;
using GatewayGrpcService.Data.Repostories;

namespace GatewayGrpcService.Services;

public class SQLMessageServices : ISQLMessageServices
{
    private readonly IGatewayGrpcMessageRepo _repo;
    private readonly ILogger<SQLMessageServices> _logger;
    public SQLMessageServices(IGatewayGrpcMessageRepo repo, ILogger<SQLMessageServices> logger)
    {
        _repo = repo;
        _logger = logger;
    }
    public async Task AddNewRsiMessage(RSI message)
    {
        _repo.Add(message);
        await _repo.UnitOfWork.SaveEntitiesAsync();
        _repo.AddCommon(message.id);
        await _repo.UnitOfWork.SaveEntitiesAsync();
    }
}
