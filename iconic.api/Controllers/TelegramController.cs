
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Models;
using System.Threading.Tasks;
using iconic.common.Services;
using iconic.common.Helpers;
using iconic.telegram;
using Telegram.Bot.Types;

[Route("api/[controller]")]
[ApiController]
public class TelegramController : Controller
{
    public IConfiguration _configuration;
    private readonly IMessageProcessor _messageProcessor;
    private readonly TelegramService _telegramService;
    public TelegramController(IConfiguration configuration, IMessageProcessor messageProcessor, TelegramService telegramService)
    {
        _configuration = configuration;
        _messageProcessor = messageProcessor;
        _telegramService = telegramService;
    }


    [HttpPost("incoming")]
    public async Task<bool> IncomingMessage(Update incomingMessage)
    {
        var response = await _messageProcessor.Process(incomingMessage.Message.Text);
        await _telegramService.SendMessage(response, incomingMessage.Message.Chat.Id);
        return true;
    }
}