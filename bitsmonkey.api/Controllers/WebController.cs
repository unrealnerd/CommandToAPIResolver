using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using bitsmonkey.common.Services;
using Models;
using Microsoft.Extensions.Options;
using bitsmonkey.common.Search;
using bitsmonkey.common.Models;

[Route("api/[controller]")]
[ApiController]
public class WebController : Controller
{
    private readonly IMessageProcessor _messageProcessor;
    // private readonly IOptions<ServicesSettings> servicesSettings;

    public WebController(IMessageProcessor messageProcessor 
    // IOptions<ServicesSettings> servicesSettings
    )
    {
        _messageProcessor = messageProcessor;
        // this.servicesSettings = servicesSettings;
    }


    [HttpPost("incoming")]
    public async Task<IActionResult> Incoming(IncomingMessage incomingMessage)
    {
        var response = await _messageProcessor.Process(incomingMessage);

        return Ok(response);
    }
}