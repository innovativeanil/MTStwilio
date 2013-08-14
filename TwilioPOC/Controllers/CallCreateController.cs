﻿using System;
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
                // find the number
                string AccountSid = "AC13f02fa06d1853607c7b7a271a973e17";
                string AuthToken = "{{ auth_token }}";
                var twilio = new TwilioRestClient(AccountSid, AuthToken);

                var number1 = twilio.GetIncomingPhoneNumber(request.CallSid);
                var number2 = twilio.GetIncomingPhoneNumber(request.DialCallSid);

                var id = DataStore.Instance.Create(
                    new Feedback
                        {
                            Submitter = "Twilio", 
                            Phone = string.Format("{0} [{1}], {2} [{3}]", request.CallSid, number1, request.DialCallSid, number2),
                            Message = recording
                        });

                response.Say(string.Format("Thank you for your feedback. Your item number is {0}. Goodbye.", id));
                response.Hangup();
            }

            response.Redirect(CallHomeController.URL);

            return this.Request.CreateResponse(
                HttpStatusCode.OK, response.Element, new XmlMediaTypeFormatter());
        }
    }
}
