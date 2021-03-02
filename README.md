# Call Tracking on ASP.NET MVC

![](https://github.com/TwilioDevEd/call-tracking-csharp/workflows/NetFx/badge.svg)

Use Twilio to track the effectiveness of your different marketing campaigns.
Learn how call tracking helps organizations in [these Twilio customer
stories](https://www.twilio.com/use-cases/call-tracking).

[Read the full tutorial here](https://www.twilio.com/docs/tutorials/walkthrough/call-tracking/csharp/mvc)!

## Quickstart

### Create a TwiML App

This project is configured to use a **TwiML App**, which allows us to easily set
the voice URLs for all Twilio phone numbers we purchase in this app.

Create a new TwiML app at https://www.twilio.com/console/phone-numbers/runtime/twiml-apps and use
its `Sid` as the `TWIML_APPLICATION_SID` environment variable wherever you run
this app.


You can learn more about TwiML apps here:
https://www.twilio.com/help/faq/twilio-client/how-do-i-create-a-twiml-app

### Local development

This project is built using the [ASP.NET MVC](http://www.asp.net/mvc) web framework.

1. First clone this repository and `cd` into its directory:
   ```
   git clone git@github.com:TwilioDevEd/call-tracking-csharp.git

   cd call-tracking-csharp
   ```

1. Create a copy of `CallTracking.Web/Local.config.sample` and rename it to
   `CallTracking.Web/Local.config`.

1. Open `CallTracking.Web/Local.config` and update the following keys:
   ```
   <appSettings>
     <add key="TwilioAccountSid" value="TWILIO_ACCOUNT_SID"/>
     <add key="TwilioAuthToken" value="TWILIO_AUTH_TOKEN"/>
     <add key="TwiMLApplicationSid" value="TWIML_APPLICATION_SID"/>
   </appSettings>
   ```

1. Build the solution.

1. Make sure you have SQL Server Express 2019 with [LocalDB support](https://docs.microsoft.com/en-us/sql/database-engine/configure-windows/sql-server-express-localdb)

1. Run `Update-Database` to execute the migrations.
   
   *(Be sure to check that your database server name matches the one from the connection string on `Web.config`. For reference, default values where used upon SQLServer installation)*

   Running the command Update-Database will run the migrations and run the Seed
   method, if you want to inspect this you can use SQL Server Object
   Explorer.

   That's it!
