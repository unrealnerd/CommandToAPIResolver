# Bot Conductor

This Application should be able to invoke a specified API based on the keyboard. eg: if `cbs\2` is sent it should call the [Corporate Bull Shit Buzzword API](https://github.com/sameerkumar18/corporate-bs-generator-api) and show the reply in any of this mediums `[Whatsapp, Telergam, WebPage]`.


Right now This application uses **Twilio whatsapp sandbox** and **Corporate Bull Shit Buzzword API** for messages sent in this format `cbs\<anything>`. 

Once you set up Twilio sandbox for you go ahead and updathe your values of the following settings.

```
"TwilioAuthToken":"VALUE",
"TwilioAccountSid":"VALUE",
"TwilioFromPhoneNumber":"VALUE"
```

This will use generate a radom BS Buzzword using [Corporate Bull Shit Buzzword API](https://github.com/sameerkumar18/corporate-bs-generator-api) as a reply on Whatsapp.

### TODO

- [ ] Integrate Telegram medium
- [ ] Containerize the application
- [ ] Integrate a web controller which will reply with value for the api invoked
- [ ] Use JWT and secure it like [this](https://dev.to/bitsmonkey/jwt-in-dotnet-core-9bg)
