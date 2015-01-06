using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GetSlides.DL;

namespace GetSlides.APP.Models
{
    public class Presentation
    {
        public Presentation(DL.Presentation presentation)
        {
            this.Id = presentation.Id;
            this.Name = presentation.Name;
            this.Picture = presentation.Picture;
            this.Info = presentation.Info;
            this.DateUploaded = presentation.DateUploaded;
            this.PresentationUri = presentation.PresentationURI;
        }

        public static List<Presentation> ToList(List<DL.Presentation> presentations)
        {
            var result = new List<Presentation>();
            foreach (var presentation in presentations)
            {
                result.Add(new Presentation(presentation));
            }
            return result;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Picture { get; set; }
        public string Info { get; set; }
        public System.DateTime DateUploaded { get; set; }
        public string UserId { get; set; }
        public string PresentationUri { get; set; }
    }
}