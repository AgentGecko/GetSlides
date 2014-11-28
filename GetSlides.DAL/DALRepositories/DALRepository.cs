using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetSlides.DAL
{
    public abstract class DALRepository<T> : IDALRepository<T> where T : IDALObject
    {
        public abstract ICollection<T> Select();
        public abstract T Select(int ID);
        public abstract void Create(T item);
        public abstract void Update(T item);
        public abstract void Delete(T item);

        protected string GenerateID() 
        {
            return Guid.NewGuid().ToString();
        }
    
    }
}
