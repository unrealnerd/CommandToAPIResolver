using System;
using Medium.WhatsApp;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Models;
using Twilio.AspNet.Common;
using Twilio.AspNet.Core;
using Twilio.TwiML;
using iconic.common.CorporateBuzzWords;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class WhatsAppController : TwilioController
{
    public IConfiguration _configuration;
    public WhatsAppController(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    [HttpPost]
    public void Post([FromBody]Message message)
    {
        Sender s = new Sender(_configuration);
        s.Message(message.Body, message.ToPhoneNumber);
    }

    [HttpPost("incoming")]    
    public async Task<TwiMLResult> IncomingMessage([FromForm]SmsRequest incomingMessage)
    {
        var messagingResponse = new MessagingResponse();

        BuzzWordGenerator generator = new BuzzWordGenerator();

        string randomBuzz = await generator.GenerateRandomBuzz();

        messagingResponse.Message("Random Corporate Gyan: " + randomBuzz);
        return TwiML(messagingResponse);
    }
}