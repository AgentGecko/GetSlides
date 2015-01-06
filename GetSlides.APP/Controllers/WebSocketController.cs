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
                    subject.presentationId = ParseData(presentationId);
                    HttpContext.Current.AcceptWebSocketRequest(subject);
                    return new HttpResponseMessage(HttpStatusCode.SwitchingProtocols);
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
        [Route("present/getpin/{presentationId}")]
        public dynamic GetSubjectPin(string presentationId)
        {
            WebSocketSubject subject = (WebSocketSubject)WebSocketFactory._subjects.Where(t => t.Value != null).FirstOrDefault(t => t.Value.GetPresentationId() == ParseData(presentationId)).Value;
            return subject.GetPin();
        }

        [HttpGet]
        [Route("geturi/{presentationPin}")]
        public dynamic GetPresentationURI(string presentationPin) 
        {
            int _pin = ParseData(presentationPin);
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
                var observer = (WebSocketObserver)WebSocketFactory.CreateObserver(ParseData(presentationPin));
                HttpContext.Current.AcceptWebSocketRequest(observer);
                return new HttpResponseMessage(HttpStatusCode.SwitchingProtocols);
            }
            else
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
        }

        private int ParseData(string pin) 
        {
            return Int32.Parse(pin);
        }
    }
    
}
