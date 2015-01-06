using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;

namespace GetSlides.APP.Utility
{
    public class CustomMIMEStreamProvider : MultipartFormDataStreamProvider
    {
        public string fileName;

        public CustomMIMEStreamProvider(string path) : base(path) {}

 
        public override string GetLocalFileName(System.Net.Http.Headers.HttpContentHeaders headers)
        {
            var name = !string.IsNullOrWhiteSpace(headers.ContentDisposition.FileName) ? headers.ContentDisposition.FileName : "NoName";
            name = name.Replace("\"", string.Empty);
            this.fileName = name;
            return name; // Chrome adds \"'s 
        }
    }
}
