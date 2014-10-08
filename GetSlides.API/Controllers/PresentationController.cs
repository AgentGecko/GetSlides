using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using GetSlides.API.Models;
using Newtonsoft.Json;
using System.Web;
using System.Threading.Tasks;
using System.Diagnostics;

namespace GetSlides.API.Controllers
{
    public class PresentationController : ApiController
    {
        [HttpGet] // Signals the caller that when there is a Http GET request at this class this method should be called
        public IEnumerable<Presentation> GetDummy()
        {
            //return new List<string> { "a", "b", "c" };
            List<Presentation> lista = new List<Presentation>();
            lista.Add(new Presentation("Prezentacija3", "", "Info neki jako jako jako jako jako jako jako jako jako jako dug.", ""));
            lista.Add(new Presentation("Prezetacija4", "", "Info4", ""));
            lista.Add(new Presentation("Prezentacija5", "", "Info5", ""));
            return lista;
        }
        public async Task<HttpResponseMessage> UploadPresentation()
        {
            if(!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }

            string root = HttpContext.Current.Server.MapPath("~/Uploads");
            var provider = new MultipartFileStreamProvider(root);
            try
            {
                await Request.Content.ReadAsMultipartAsync(provider);

                foreach (MultipartFileData pdfFile in provider.FileData)
                {
                    Trace.WriteLine(pdfFile.Headers.ContentDisposition.FileName);
                    Trace.WriteLine("Server path: " + pdfFile.LocalFileName);
                }
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception error)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, error);
            }
        }
    }
}
