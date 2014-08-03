using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetSlides.BLL
{
    public interface IRepository<T> where T: IBLLObject
    {
        ICollection<T> Select();
        T Select(string ID);
    }
}
