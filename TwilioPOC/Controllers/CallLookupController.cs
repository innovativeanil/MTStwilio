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

namespace TwilioPOC.Controllers
{
    public class CallLookupController : ApiController
    {
        public const string URL = Constants.ControllerDirectory + "CallLookup";

        [HttpPost]
        public HttpResponseMessage Post(VoiceRequest request)
        {
            var response = new TwilioResponse();
            var option = (request != null) ? request.Digits : string.Empty;

            var item = DataStore.Instance.GetItem(option);

            if (item == null)
            {
                response.Say("Item not found.");
            } else
            {
                response.Say(string.Format("The status of item {0} is {1}.", item.Id, item.Status));
            }

            response.Redirect(CallHomeController.URL);
            
            return this.Request.CreateResponse(
                HttpStatusCode.OK, response.Element, new XmlMediaTypeFormatter());
        }
    }
}
