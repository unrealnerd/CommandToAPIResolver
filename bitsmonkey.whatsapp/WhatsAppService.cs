using Microsoft.Extensions.Options;
using Twilio;
using Twilio.Exceptions;
using Twilio.Rest.Api.V2010.Account;

namespace bitsmonkey.whatsapp
{
    public class WhatsAppService
    {
        public IOptions<WhatsAppOptions> _whatsAppOptions { get; set; }
        public WhatsAppService(IOptions<WhatsAppOptions> whatsAppOptions)
        {
            _whatsAppOptions = whatsAppOptions;
        }
        public void SendMessage(string body, string phoneNumber)
        {           

            TwilioClient.Init(_whatsAppOptions.Value.TwilioAccountSid, _whatsAppOptions.Value.TwilioAuthToken);

            try
            {
                var returnedMessage = MessageResource.Create(
                    body: body,
                    from: new Twilio.Types.PhoneNumber(_whatsAppOptions.Value.TwilioFromPhoneNumber),
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