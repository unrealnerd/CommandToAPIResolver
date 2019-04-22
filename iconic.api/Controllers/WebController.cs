
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using iconic.common.Services;

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
    public async Task<IActionResult> IncomingMessage([FromBody]string incomingMessage)
    {
        var response = await _messageProcessor.Process(incomingMessage);
        
        return Ok(response);
    }
}