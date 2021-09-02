using Model.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class GalleryDao
    {
        MyBlogDbContext db = null;
        public GalleryDao()
        {
            db = new MyBlogDbContext();
        }
        public long Insert(Gallery entity)
        {
            db.Gallerys.Add(entity);
            db.SaveChanges();
            return entity.ID;
        }
        public bool Update(Gallery entity)
        {
            try
            {
                var gallery = db.Gallerys.Find(entity.ID);
                gallery.Link = entity.Link;
                gallery.ImageUrl = entity.ImageUrl;
                gallery.ModifiedBy = entity.ModifiedBy;
                gallery.ModifiedDate = DateTime.Now;
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool Delete(long id)
        {
            try
            {
                var gallery = db.Gallerys.Find(id);
                db.Gallerys.Remove(gallery);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public IEnumerable<Gallery> ListAllPaging(int page, int pageSize)
        {
            return db.Gallerys.OrderByDescending(x => x.CreateDate).ToPagedList(page, pageSize);
        }
        public IEnumerable<Gallery> ListAllGallery()
        {
            var query = from tempUsers in db.Gallerys
                        select tempUsers;
            return query;
        }
        public Gallery GetbyID(long id)
        {
            return db.Gallerys.SingleOrDefault(x => x.ID == id);
        }
        public Gallery ViewDetail(long id)
        {
            return db.Gallerys.Find(id);
        }
        public bool ChangeStatus(long id)
        {
            var gallery = db.Gallerys.Find(id);
            gallery.Status = !gallery.Status;
            db.SaveChanges();
            return gallery.Status;
        }
    }
}
