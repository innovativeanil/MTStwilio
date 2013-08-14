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
    public class CallHomeController : ApiController
    {
        [HttpPost]
        public HttpResponseMessage Post(VoiceRequest request)
        {
            var response = new TwilioResponse();

            response.Say("Welcome to the MTS mobile feedback reporting system.  Press 1 to enter feedback. Press 2 to look up an item.");
            response.Gather(new { numDigits = 1, action = string.Format("/api/CallHome/Route") });

            return this.Request.CreateResponse(
                HttpStatusCode.OK, response.Element, new XmlMediaTypeFormatter());
        }
        
        [HttpPost]
        public HttpResponseMessage Route(VoiceRequest request)
        {
            var response = new TwilioResponse();

            response.Say("You Pressed something! Bitch.");

            return this.Request.CreateResponse(
                HttpStatusCode.OK, response.Element, new XmlMediaTypeFormatter());
        }
    }
}
