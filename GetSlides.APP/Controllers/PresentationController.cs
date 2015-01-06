using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Net.Http;
using System.Web.Http;
using GetSlides.DL;
using GetSlides.APP.Repositories;
using GetSlides.APP.Utility;

namespace GetSlides.APP.Controllers
{
    [Route("api/presentation")]
    public class PresentationController : ApiController
    {
        public PresentationController() { }

        [Authorize]
        [Route("presentations")]
        public List<Presentation> GetUserPresentations()
        {
            List<Presentation> UserPresentations = null;
            using (PresentationRepository repo = new PresentationRepository())
            {
                UserPresentations = repo.SelectByUsername(User.Identity.Name);
            }
            return UserPresentations;
        }

        [Authorize]
        [Route("delete")]
        public void DeletePresentation(int presentationId)
        {
            using (PresentationRepository presentationRepository = new PresentationRepository())
            {
                if(presentationRepository.CheckPresentationOwner(presentationId, User.Identity.Name))
                    presentationRepository.Delete(presentationId);
            }
        }
        
        [HttpPost]
        [Authorize]
        [Route("upload")]
        public dynamic UploadPresentation()
        {
            if (!Request.Content.IsMimeMultipartContent())
            {
                return new HttpResponseMessage(HttpStatusCode.UnsupportedMediaType);
            }
            
            string root = HttpContext.Current.Server.MapPath("~/PDF");
            var provider = new CustomMIMEStreamProvider(root);

            Request.Content.ReadAsMultipartAsync(provider);

            return GetUserPresentations();
        }

        [Authorize]
        [Route("edit/{presentationId}/name/{name}/info/{info}")]
        public void EditPresentation(int presentationId, string name, string info)
        {
            using(PresentationRepository presentationRepository = new PresentationRepository())
            {
                if(presentationRepository.CheckPresentationOwner(presentationId,User.Identity.Name))
                {
                    Presentation presentation = new Presentation();
                    presentation.Name = name;
                    presentation.Info = info;
                    presentation.Id = presentationId;
                    presentationRepository.Update(presentation);
                }
            }
        }

        protected void AddPresentation(Presentation presentation, string userName)
        {
            using (PresentationRepository presentationRepository = new PresentationRepository())
            {
                presentationRepository.Create(presentation, userName);
            }
        }



    }
}
