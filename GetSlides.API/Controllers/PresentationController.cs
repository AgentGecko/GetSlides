using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using GetSlides.API.Models;
using Newtonsoft.Json;

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
    }
}
