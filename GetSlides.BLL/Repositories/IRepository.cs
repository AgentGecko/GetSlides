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
        void Update(T item);
        void Delete(T item);
        void Create(T item);
    }
}
