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
            current = presentation;
            context.SaveChanges();
        }
        public void UpdateInfo(int presentationId, string newInfo)
        {
            Presentation current = context.Presentations.FirstOrDefault(t => t.Id == presentationId);
            current.Info = newInfo;
            context.SaveChanges();
        }
        public void UpdateName(int presentationId, string newName)
        {
            Presentation current = context.Presentations.FirstOrDefault(t => t.Id == presentationId);
            current.Info = newName;
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

        public void Dispose()
        {
            context.Dispose();
            context = null;
        }
    }
}