
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
    public async Task<bool> IncomingMessage(AppMention incomingMessage)
    {
        var response = await _messageProcessor.Process(incomingMessage.Text);
        await SlackService.SendMessage(response, incomingMessage.Message.Chat.Id);
        return true;
    }
}