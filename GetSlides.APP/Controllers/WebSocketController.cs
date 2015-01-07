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
        [Route("present/{presentationId}/username/{userName}")]
        public dynamic GetSubject(string presentationId, string userName)
        {
            if (HttpContext.Current.IsWebSocketRequest)
            {
                try
                { 
                    string UserPin;
                    Presentation userPresentation;

                    using(PresentationRepository presentationRepository = new PresentationRepository())
                    {
                        userPresentation = presentationRepository.Select(Convert.ToInt32(presentationId));
                    }
                   
                    var subject = (WebSocketSubject) WebSocketFactory.CreateSubject(userName, out UserPin, userPresentation.PresentationURI);
                    subject.presentationId = Convert.ToInt32(presentationId);
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
        [Authorize]
        [Route("present/getpin/{presentationId}")]
        public dynamic GetSubjectPin(string presentationId)
        {
            WebSocketSubject subject = (WebSocketSubject)WebSocketFactory._subjects.Where(t => t.Value != null).FirstOrDefault(t => t.Value.GetPresentationId() == Convert.ToInt32(presentationId)).Value;
            return subject.GetPin();
        }

        [HttpGet]
        [Route("geturi/{presentationPin}")]
        public dynamic GetPresentationURI(string presentationPin) 
        {
            string pin = presentationPin;
            if (WebSocketFactory.IsActive(pin))
            {

               WebSocketSubject subject = (WebSocketSubject) WebSocketFactory._subjects.FirstOrDefault(t => t.Key == pin).Value;
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
                var observer = (WebSocketObserver)WebSocketFactory.CreateObserver(presentationPin);
                WebSocketSubject sub = (WebSocketSubject)WebSocketFactory._subjects.FirstOrDefault(t => t.Key == presentationPin).Value;
                observer.lastpage = sub.lastPage;
                HttpContext.Current.AcceptWebSocketRequest(observer);
                return new HttpResponseMessage(HttpStatusCode.SwitchingProtocols);
            }
            else
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
        }

    }
    
}
