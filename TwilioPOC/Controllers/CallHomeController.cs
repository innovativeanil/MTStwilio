using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web.Http;
using Twilio.Mvc;
using Twilio.TwiML;
using Twilio.TwiML.Mvc;

namespace TwilioPOC.Controllers
{
    public class CallHomeController : ApiController
    {
        public const string URL = Constants.ControllerDirectory + "CallHome";

        [HttpPost]
        public HttpResponseMessage Post(VoiceRequest request)
        {
            var response = new TwilioResponse();

            response.Say("Welcome to the MTS mobile feedback reporting system.");
            response.Say("Press 1 to enter feedback.");
            response.Say("Press 2 to look up an item.");
            response.Gather(new { numDigits = 1, action = CallRoutingController.URL });

            return this.Request.CreateResponse(
                HttpStatusCode.OK, response.Element, new XmlMediaTypeFormatter());
        }
    }
}
