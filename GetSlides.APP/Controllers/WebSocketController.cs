using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Net.Http;
using System.Web.Http;
using Microsoft.Web.WebSockets;
using GetSlides.APP.WebSocket;
using GetSlides.APP.Repositories;
using GetSlides.DL;

namespace GetSlides.APP.Controllers
{
    [RoutePrefix("api/ws")]
    public class WebSocketController : ApiController
    {
        [HttpGet]
        //[Authorize]
        [Route("present/{presentationId}")]
        public dynamic GetSubject(string presentationId)
        {
            if (HttpContext.Current.IsWebSocketRequest)
            {
                try
                { 
                    int UserPin;
                    Presentation userPresentation;

                    using(PresentationRepository presentationRepository = new PresentationRepository())
                    {
                        userPresentation = presentationRepository.Select(Int32.Parse(presentationId));
                    }
                   
                    var subject = (WebSocketSubject) WebSocketFactory.CreateSubject(User.Identity.Name, out UserPin, userPresentation.PresentationURI);
                    HttpContext.Current.AcceptWebSocketRequest(subject);
                    HttpContext.Current.Response.StatusCode = (int) HttpStatusCode.SwitchingProtocols;
                    return UserPin;
                }
                catch (Exception ex) 
                {
                    throw ex;
                }
                
            }
            else
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
        }


        [HttpGet]
        [Route("geturi/{presentationPin}")]
        public dynamic GetPresentationURI(string presentationPin) 
        {
            int _pin = GetPin(presentationPin);
            if (WebSocketFactory.IsActive(_pin))
            {

               WebSocketSubject subject = (WebSocketSubject) WebSocketFactory._subjects.FirstOrDefault(t => t.Key == _pin).Value;
               return subject.presentationUri;
            }
            else
                return "Inactive pin, please try again.";
        }

        [HttpGet]
        [Route("watch/{presentationPin}")]
        public HttpResponseMessage GetObserver(string presentationPin)
        {
            if (HttpContext.Current.IsWebSocketRequest)
            {
                var observer = (WebSocketObserver)WebSocketFactory.CreateObserver(GetPin(presentationPin));
                HttpContext.Current.AcceptWebSocketRequest(observer);
                return new HttpResponseMessage(HttpStatusCode.SwitchingProtocols);
            }
            else
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
        }

        private int GetPin(string pin) 
        {
            return Int32.Parse(pin);
        }
    }
    
}
