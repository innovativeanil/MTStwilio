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
    public class CallCreateController : ApiController
    {
        public const string URL = Constants.ControllerDirectory + "CallCreate";

        [HttpPost]
        public HttpResponseMessage Post(VoiceRequest request)
        {
            var response = new TwilioResponse();
            var name = (request != null) ? request.TranscriptionText : "Response is null";

            response.Say(string.Format("You said {0} right? Good.", name));
            response.Redirect(CallHomeController.URL);

            return this.Request.CreateResponse(
                HttpStatusCode.OK, response.Element, new XmlMediaTypeFormatter());
        }
    }
}
