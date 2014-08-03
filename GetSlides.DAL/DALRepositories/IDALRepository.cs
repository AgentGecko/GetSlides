using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetSlides.DAL
{
    public interface IDALRepository<T> where T : IDALObject
    {
        void Create(T item);
        ICollection<T> Select();
        T Select(string ID);
        void Update(T item);
        void Delete(T item);
    }
}
