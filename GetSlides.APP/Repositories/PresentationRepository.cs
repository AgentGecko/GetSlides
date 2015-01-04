using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using  GetSlides.DL;

namespace GetSlides.APP.Repositories
{
    public class PresentationRepository: IDisposable
    {
        public GetSlidesDB context;
        public PresentationRepository()
        {
            this.context = new GetSlidesDB();
        }

        #region CRUD
        public void Create(Presentation presentation, string userName)
        {
            presentation.AspNetUser = context.AspNetUsers.FirstOrDefault(t => t.UserName == userName);
            context.Presentations.Add(presentation);
            context.SaveChanges();
        }

        public List<Presentation> Select()
        {
            return context.Presentations.ToList();
        }
        public Presentation Select(int id)
        {
            return context.Presentations.FirstOrDefault(t => t.Id == id);
        }
        public Presentation Select(String userId)
        {
            return context.Presentations.FirstOrDefault(t => t.UserId == userId);
        }

        public void Update(Presentation presentation)
        {
            Presentation current = context.Presentations.FirstOrDefault(t => t.Id == presentation.Id);
            current = presentation;
            context.SaveChanges();
        }

        public void Delete(Presentation presentation)
        {
            context.Presentations.Remove(presentation);
            context.SaveChanges();
        }
        #endregion

        public void Dispose()
        {
            context.Dispose();
            context = null;
        }
    }
}