
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using iconic.common.Services;
using iconic.slack;
using iconic.slack.Models;

[Route("api/[controller]")]
[ApiController]
public class SlackController : Controller
{
    public IConfiguration _configuration;
    private readonly IMessageProcessor _messageProcessor;
    private readonly SlackService SlackService;
    public SlackController(IConfiguration configuration, IMessageProcessor messageProcessor, SlackService slackService)
    {
        _configuration = configuration;
        _messageProcessor = messageProcessor;
        SlackService = slackService;
    }


    [HttpPost("incoming")]
    public async Task<dynamic> IncomingMessage(Message incomingMessage)
    {
        if (incomingMessage.type == "url_verification")
        {
            //TODO: Verify the signature if its from slack
            return Ok(new { Challenge = incomingMessage.challenge });
        }
        else if (incomingMessage.@event.type == "message" && !string.IsNullOrEmpty(incomingMessage.@event.user))
        {
            var response = await _messageProcessor.Process(incomingMessage.@event.text);
            await SlackService.SendMessage(response, incomingMessage.@event.channel);
            return Ok();
        }
        return Ok();
    }    
}