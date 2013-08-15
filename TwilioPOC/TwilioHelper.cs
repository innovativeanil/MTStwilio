using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Twilio;

namespace TwilioPOC
{
    public class TwilioHelper
    {
        public static string GetNumber(string sid)
        {
            // find the number
            //string AuthToken = "{{ " + Constants.TwilioToken + " }}";
            var twilio = new TwilioRestClient(Constants.TwilioSid, Constants.TwilioToken);
            var call = twilio.GetCall(sid);
            return call.From;
            //return twilio.GetIncomingPhoneNumber(sid);
        }

        public static bool SendText(string number, string message)
        {
            var client = new TwilioRestClient(Constants.TwilioSid, Constants.TwilioToken);
            // Send a new outgoing SMS by POSTing to the SMS resource */
            client.SendSmsMessage(
                Constants.TwilioNumber, // From number, must be an SMS-enabled Twilio number 
                number,         // To number, if using Sandbox see note above
                message
            );

            return true;
        }
    }
}