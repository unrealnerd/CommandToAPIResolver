
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using bitsmonkey.common.Services;
using bitsmonkey.slack;
using bitsmonkey.slack.Models;

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
        if (incomingMessage.Type == "url_verification")
        {
            //TODO: Verify the signature if its from slack
            return Ok(new { incomingMessage.Challenge });
        }
        else if (incomingMessage.@Event.Type == "message" && !string.IsNullOrEmpty(incomingMessage.@Event.User))
        {
            var response = await _messageProcessor.Process(incomingMessage.@Event.Text);
            await SlackService.SendMessage(response, incomingMessage.@Event.Channel);
            return Ok();
        }
        return Ok();
    }    
}