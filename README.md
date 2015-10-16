# Call Tracking on ASP.NET MVC

[![Build status](https://ci.appveyor.com/api/projects/status/k6yep2jsiwefbsw9?svg=true)](https://ci.appveyor.com/project/TwilioDevEd/call-tracking-csharp)

Use Twilio to track the effectiveness of your different marketing campaigns.
Learn how call tracking helps organizations in [these Twilio customer
stories](https://www.twilio.com/use-cases/call-tracking).

## Quickstart

### Create a TwiML App

This project is configured to use a **TwiML App**, which allows us to easily set
the voice URLs for all Twilio phone numbers we purchase in this app.

Create a new TwiML app at https://www.twilio.com/user/account/apps/add and use
its `Sid` as the `TWIML_APPLICATION_SID` environment variable wherever you run
this app.

![Creating a TwiML
App](http://howtodocs.s3.amazonaws.com/call-tracking-twiml-app.gif)

You can learn more about TwiML apps here:
https://www.twilio.com/help/faq/twilio-client/how-do-i-create-a-twiml-app

### Local development

This project is built using the [ASP.NET MVC](http://www.asp.net/mvc) web framework.

1. First clone this repository and `cd` into its directory:
   ```
   git clone git@github.com:TwilioDevEd/call-tracking-csharp.git

   cd call-tracking-csharp
   ```

2. Create a copy of `CallTracking.Web/Web.config.sample` and rename it to
   `CallTracking.Web/Web.config`.

3. Open `CallTracking.Web/Web.config` and update the following keys:
   ```
   <appSettings>
     <!-- omitted for clarity -->
     <add key="TwilioAccountSid" value="TWILIO_ACCOUNT_SID"/>
     <add key="TwilioAuthToken" value="TWILIO_AUTH_TOKEN"/>
     <add key="TwiMLApplicationSid" value="TWIML_APPLICATION_SID"/>
   </appSettings>
   ```

4. Build the solution.

5. Run `Update-Database` to execute the migrations.

   Running the command Update-Database will run the migrations and run the Seed
   method, if you want to inspect this you can use SQL Server Object
   Explorer.

  ![Call Tracking Setup](http://howtodocs.s3.amazonaws.com/call-tracking-setup.gif)

   That's it!
