using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web.Http;
using Twilio.Mvc;
using Twilio.TwiML;
using TwilioPOC.Data;
using TwilioPOC.Models;

namespace TwilioPOC.Controllers
{
    public class CallCreateController : ApiController
    {
        public const string URL = Constants.ControllerDirectory + "CallCreate";

        [HttpPost]
        public HttpResponseMessage Post(VoiceRequest request)
        {
            var response = new TwilioResponse();
            var transc = (request != null) ? request.TranscriptionText : "Request is null";
            var record = (request != null) ? request.RecordingUrl : "Request is null";

            if (request != null)
            {
                DataStore.Instance.Create(new Feedback { Submitter = "Twilio", Phone = request.CallerName, Message = record });

                response.Say(string.Format("Thanks!"));
            }

            response.Redirect(CallHomeController.URL);

            return this.Request.CreateResponse(
                HttpStatusCode.OK, response.Element, new XmlMediaTypeFormatter());
        }
    }
}
