using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetSlides.APP.WebSocket
{
    public interface IObserver
    {
        void Update(string msg);
    }
}
