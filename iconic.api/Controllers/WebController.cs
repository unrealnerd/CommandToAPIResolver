using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using iconic.common.Services;
using Models;

[Route("api/[controller]")]
[ApiController]
public class WebController : Controller
{    
    private readonly IMessageProcessor _messageProcessor;    
    public WebController(IMessageProcessor messageProcessor)
    {        
        _messageProcessor = messageProcessor;        
    }


    [HttpPost("incoming")]
    public async Task<IActionResult> Incoming(IncomingMessage incomingMessage)
    {
        var response = await _messageProcessor.Process(incomingMessage.Message);
        
        return Ok(response);
    }
}