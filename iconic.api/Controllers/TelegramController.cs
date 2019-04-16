using Medium.WhatsApp;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Models;
using System.Threading.Tasks;
using iconic.common.Services;
using iconic.common.Helpers;
using Telegram.Bot.Types;

[Route("api/[controller]")]
[ApiController]
public class TelegramController : Controller
{
    public IConfiguration _configuration;
    private readonly IMessageProcessor _messageProcessor;
    public TelegramController(IConfiguration configuration, IMessageProcessor messageProcessor)
    {
        _configuration = configuration;
        _messageProcessor = messageProcessor;
    }

    [HttpPost]
    public void Post([FromBody]Message message)
    {
        Sender s = new Sender(_configuration);
    }

    [HttpPost("incoming")]
    public async Task IncomingMessage([FromForm]Update incomingMessage)
    {
        var response = await _messageProcessor.Process(incomingMessage.Message.Text);
    }
}