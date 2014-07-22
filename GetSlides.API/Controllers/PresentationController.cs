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
            lista.Add(new Presentation("Moje ime", "", "Info moj je da Davida volim", ""));
            lista.Add(new Presentation("Nesto", "", "Gospodine Abrame ja vas voljam (cat)", ""));
            lista.Add(new Presentation("Randomsranje", "", "lovelovelove", ""));
            return lista;
        }
    }
}
