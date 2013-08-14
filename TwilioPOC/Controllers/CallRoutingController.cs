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
        public const string URL = Constants.ControllerDirectory + "CallRouting";

        [HttpPost]
        public HttpResponseMessage Post(VoiceRequest request)
        {
            var response = new TwilioResponse();
            var option = (request != null) ? request.Digits : string.Empty;

            switch(option)
            {
                case "1":
                    response.Say("Please say your name followed by pound.");
                    response.Record(new { playBeep = "true", transcribe = "true", finishOnKey = "#", action = CallCreateController.URL });
                    break;
                case "2":
                    response.Say("Enter the feedback id number followed by pound.");
                    response.Gather(new { finishOnKey = "#", action = CallLookupController.URL });
                    break;
                default:
                    response.Say("You've entered an invalid option.");
                    response.Redirect(CallHomeController.URL);
                    break;
            }
            
            return this.Request.CreateResponse(
                HttpStatusCode.OK, response.Element, new XmlMediaTypeFormatter());
        }
    }
}
