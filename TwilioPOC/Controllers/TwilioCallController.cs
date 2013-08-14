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
    public class TwilioCallController : ApiController
    {
        [HttpPost]
        public HttpResponseMessage Post(VoiceRequest request)
        {
            var response = new TwilioResponse();

            response.Say("Welcome to this Twilio demo app. Please enter your 5 digit ID.");
            //response.Gather(new { numDigits = 5, action = string.Format("/api/Authenticate") });

            return this.Request.CreateResponse(
                HttpStatusCode.OK, response.Element, new XmlMediaTypeFormatter());
        }
    }
}
