using GlobalIntegrationApi.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GlobalIntegrationApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class StopConsumerController : ControllerBase
{
    private readonly IGlobalIntegrationServices _globalIntegrationServices;

    public StopConsumerController(IGlobalIntegrationServices globalIntegrationServices)
    {
        _globalIntegrationServices = globalIntegrationServices;
    }
    
    // POST api/<ConsumerController>
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] string consumerId)
    {
        try
        {
           await _globalIntegrationServices.StopNamedCosumer(consumerId);
        }
        catch (Exception ex)
        {
            return BadRequest();
            throw new Exception(ex.Message);           
        }
        return Ok();
    }
}
