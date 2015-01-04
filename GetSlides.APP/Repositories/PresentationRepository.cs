using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GetSlides.DL;

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
        public List<Presentation> Select(String userId)
        {
            return context.Presentations.Where(t => t.UserId == userId).ToList();
        }
        public List<Presentation> SelectByUsername(String userName)
        {
            return context.AspNetUsers.FirstOrDefault(t => t.UserName == userName).Presentations.ToList();
        }

        public void Update(Presentation presentation)
        {
            Presentation current = context.Presentations.FirstOrDefault(t => t.Id == presentation.Id);
            if (presentation.Name != null)
                current.Name = presentation.Name;
            if (presentation.Info != null)
                current.Info = presentation.Info;
            context.SaveChanges();
        }
       

        public void Delete(Presentation presentation)
        {
            context.Presentations.Remove(presentation);
            context.SaveChanges();
        }
        public void Delete(int presentationId)
        {
            Presentation presentation = context.Presentations.FirstOrDefault(t => t.Id == presentationId);
            context.Presentations.Remove(presentation);
            context.SaveChanges();
        }
        #endregion

        public bool CheckPresentationOwner(int presentationId, string userName)
        {
           return context.AspNetUsers.FirstOrDefault(t => t.UserName == userName).Presentations.Any(t => t.Id == presentationId);
        }

        public void Dispose()
        {
            context.Dispose();
            context = null;
        }
    }
}