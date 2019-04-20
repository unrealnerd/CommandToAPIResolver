using Microsoft.Extensions.Configuration;
using Twilio;
using Twilio.Clients;
using Twilio.Exceptions;
using Twilio.Rest.Api.V2010.Account;

namespace iconic.whatsapp
{
    public class Sender
    {
        public IConfiguration _configuration { get; set; }
        public Sender(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public void SendMessage(string body, string phoneNumber)
        {
            string accountSid = _configuration["TwilioAccountSid"];
            string authToken = _configuration["TwilioAuthToken"];

            TwilioClient.Init(accountSid, authToken);

            try
            {
                var returnedMessage = MessageResource.Create(
                    body: body,
                    from: new Twilio.Types.PhoneNumber(_configuration["TwilioFromPhoneNumber"]),
                    to: new Twilio.Types.PhoneNumber(phoneNumber)
                );

            }
            catch (ApiException e)
            {
                throw e;
            }


        }
    }
}