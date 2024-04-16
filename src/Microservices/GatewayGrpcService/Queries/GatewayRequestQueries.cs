using Dapper;
using System.Data.SqlClient;
using System.Data;

namespace GatewayGrpcService.Queries
{
    public class GatewayRequestQueries : IGatewayRequestQueries
    {
        private string _connectionString = string.Empty;
        public GatewayRequestQueries(string constr)
        {
            _connectionString = !string.IsNullOrWhiteSpace(constr) ? constr : throw new ArgumentNullException(nameof(constr));
        }

        public async Task<IEnumerable<RSIMessage>> GetRSIMEssagesFromDbAsync()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                if (connection.State == ConnectionState.Closed)
                {
                    try
                    {
                        connection.Open();
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.Message);
                    }
                }
                return await connection.QueryAsync<RSIMessage>("dbo.spGetRsiMessages", commandType: CommandType.StoredProcedure);
            }     
        }
    }
}
