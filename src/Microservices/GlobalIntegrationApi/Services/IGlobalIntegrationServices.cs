
namespace GlobalIntegrationApi.Services
{
    public interface IGlobalIntegrationServices
    {
        Task<bool> StopNamedCosumer(string consumerId);

        Task<bool> RestartNamedCosumer(string consumerId);
    }
}