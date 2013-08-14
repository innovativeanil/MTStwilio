using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web.Http;
using Twilio.Mvc;
using Twilio.TwiML;

namespace TwilioPOC.Controllers
{
    public class CallRoutingController : ApiController
    {
        [HttpPost]
        public HttpResponseMessage Post(VoiceRequest request)
        {
            var response = new TwilioResponse();
            var option = (request != null) ? request.Digits : string.Empty;

            switch(option)
            {
                case "1":
                    response.Say("Please say your name.");
                    response.Record(new { playBeep = "true", transcribe = "true", finishOnKey = "#" });
                    break;
                case "2":
                    response.Say("Enter the feedback id number.");
                    response.Gather(new { finishOnKey = "#", action = string.Format("/api/CallLookup") });
                    break;
                default:
                    response.Say("You've entered an invalid option.");
                    response.Redirect("api/CallHome");
                    break;
            }

            response.Say("You are a little bitch, bitch.");

            return this.Request.CreateResponse(
                HttpStatusCode.OK, response.Element, new XmlMediaTypeFormatter());
        }
    }
}
