using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Net;
using System.Web;
using System.Net.Http;
using System.Web.Http;
using System.Web.Script.Serialization;
using GetSlides.DL;
using GetSlides.APP.Repositories;
using GetSlides.APP.Storage;
using GetSlides.APP.Utility;

namespace GetSlides.APP.Controllers
{
    [RoutePrefix("api/presentation")]
    public class PresentationController : ApiController
    {
        public PresentationController() { }

        [Authorize]
        [Route("get")]
        public dynamic GetUserPresentations()
        {
            List<Presentation> UserPresentations = null;
            using (PresentationRepository repo = new PresentationRepository())
            {
                UserPresentations = repo.SelectByUsername(User.Identity.Name);
            }
            return Models.Presentation.ToList(UserPresentations);
        }

        [Authorize]
        [Route("get/{id}")]
        public dynamic GetUserPresentation(int id)
        {
            using (PresentationRepository repo = new PresentationRepository())
            {
                return new Models.Presentation(repo.Select(id));
            }
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
        public async Task<dynamic> UploadPresentation()
        {
            if (!Request.Content.IsMimeMultipartContent())
            {
                return new HttpResponseMessage(HttpStatusCode.UnsupportedMediaType);
            }
            
            string root = HttpContext.Current.Server.MapPath("~/PDF");
            var provider = new CustomMIMEStreamProvider(root);
            await Request.Content.ReadAsMultipartAsync(provider);

            //var result = await BlobStorage.SavePdfToBlob(await Request.Content.ReadAsStreamAsync(), "bla.pdf");


            
            Presentation uploadedPresentation = new Presentation(provider.fileName, "http://localhost:6316/PDF/"+ provider.fileName);

            AddPresentation(uploadedPresentation, User.Identity.Name);
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
