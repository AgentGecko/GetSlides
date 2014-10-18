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
using System.IO;

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

        public Task<HttpResponseMessage> PostFile()
        {
            HttpRequestMessage request = this.Request;
            if (!request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }

            string root = System.Web.HttpContext.Current.Server.MapPath("~/Uploads");
            MultipartFormDataStreamProvider provider = new MultipartFormDataStreamProvider(root);

            var task = Request.Content.ReadAsMultipartAsync(provider);

            return task.ContinueWith<HttpResponseMessage>(t =>
                {
                    var bodyPart = provider.FileData.FirstOrDefault();
                    string savedFile = bodyPart.ToString().TrimStart('"').TrimEnd('"');

                    FileInfo file = new FileInfo(savedFile);
                    file.CopyTo(Path.Combine(root, savedFile), true);

                    return new HttpResponseMessage()
                    {
                        Content = new StringContent("File uploaded.")
                    };
                }, TaskScheduler.FromCurrentSynchronizationContext());
        }
    }
}
