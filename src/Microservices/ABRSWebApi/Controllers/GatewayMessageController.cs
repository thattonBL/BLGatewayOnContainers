using ABRSWebApi.Application.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ABRSWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GatewayMessageController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<GatewayMessageController> _logger;

        public GatewayMessageController(IMediator mediator, ILogger<GatewayMessageController> logger)
        {
            _mediator = mediator ?? throw new ArgumentException(nameof(mediator));
            _logger = logger ?? throw new ArgumentException(nameof(logger));
        }

        // POST api/<GatewayMessageController>
        [Route("rsi")]       
        [HttpPost]
        public async Task<ActionResult<bool>> CreateRsiMessageFromInput([FromBody] AddNewRsiMessageCommand addRsiMessageCommand)
        {
            //TODO: Add logging
            return await _mediator.Send(addRsiMessageCommand);
        }
    }
}
