using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web.Http;
using Twilio;
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
            var transcrib = (request != null) ? request.TranscriptionText : "Request is null";
            var recording = (request != null) ? request.RecordingUrl : "Request is null";
            
            if (request != null)
            {
                var number = TwilioHelper.GetNumber(request.CallSid);
                var id = DataStore.Instance.Create(
                    new Feedback
                        {
                            Submitter = "Twilio", 
                            Phone = number,
                            Message = recording
                        });

                response.Say("Thank you for your feedback.");
                response.Say(string.Format("Your item number is {0}. Goodbye.", id));
                response.Hangup();
            }

            response.Redirect(CallHomeController.URL);

            return this.Request.CreateResponse(
                HttpStatusCode.OK, response.Element, new XmlMediaTypeFormatter());
        }
    }
}
