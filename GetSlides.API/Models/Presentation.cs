using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

// Dummie data
namespace GetSlides.API.Models
{
    public class Presentation
    { 
        public string Name {get;set;}
        public string Picture {get;set;} 
        public string Info {get;set;} 
        public string DateUploaded {get;set;}

        public Presentation(string name,string pic,string info,string dateUploaded)
        {
            this.Name = name;
            this.Picture = pic;
            this.Info = info;
            this.DateUploaded = dateUploaded;
        }
    
    }
}