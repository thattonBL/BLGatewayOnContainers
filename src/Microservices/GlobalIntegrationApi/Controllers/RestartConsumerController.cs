using GlobalIntegrationApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GlobalIntegrationApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RestartConsumerController : ControllerBase
{
    private readonly IGlobalIntegrationServices _globalIntegrationServices;

    public RestartConsumerController(IGlobalIntegrationServices globalIntegrationServices)
    {
        _globalIntegrationServices = globalIntegrationServices;
    }

    // POST api/<ConsumerController>
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] string consumerId)
    {
        try
        {
           await _globalIntegrationServices.RestartNamedCosumer(consumerId);
        }
        catch (Exception ex)
        {
            return BadRequest();
            throw new Exception(ex.Message);           
        }
        return Ok();
    }
}
