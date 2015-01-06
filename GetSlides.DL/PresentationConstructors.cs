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
        public Presentation(string name, string presentationURI)
        { 
            this.Name = name;
            this.PresentationURI = presentationURI;
            this.DateUploaded = DateTime.Now;
        }
    }
}
