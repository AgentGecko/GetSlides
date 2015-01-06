using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetSlides.DL
{
    public partial class Presentation
    {
        public Presentation()
        { }
        public Presentation(string _name, string _presentationURI)
        { 
            this.Name = _name;
            this.PresentationURI = _presentationURI;
            this.DateUploaded = DateTime.Now;
        }
    }
}
