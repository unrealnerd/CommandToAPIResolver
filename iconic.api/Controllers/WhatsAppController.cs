using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Models;
using Twilio.AspNet.Common;
using Twilio.AspNet.Core;
using Twilio.TwiML;
using System.Threading.Tasks;
using iconic.common.Services;
using iconic.common.Helpers;
using iconic.whatsapp;

[Route("api/[controller]")]
[ApiController]
public class WhatsAppController : TwilioController
{
    public IConfiguration _configuration;
    private readonly IMessageProcessor _messageProcessor;
    private readonly WhatsAppService _whatsAppService;
    public WhatsAppController(IConfiguration configuration, IMessageProcessor messageProcessor, WhatsAppService whatsAppService)
    {
        _configuration = configuration;
        _messageProcessor = messageProcessor;
        _whatsAppService = whatsAppService;
    }

    [HttpPost]
    public void Post([FromBody]WhatsAppMessage message)
    {        
        _whatsAppService.SendMessage(message.Body, message.ToPhoneNumber);
    }

    [HttpPost("incoming")]    
    public async Task<TwiMLResult> IncomingMessage([FromForm]SmsRequest incomingMessage)
    {
        var messagingResponse = new MessagingResponse();

        var response = await _messageProcessor.Process(incomingMessage.Body);
        
        return TwiML(new MessagingResponse().Message(response));
    }
}