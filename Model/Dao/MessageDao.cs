using Model.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class MessageDao
    {
        MyBlogDbContext db = null;
        public MessageDao()
        {
            db = new MyBlogDbContext();
        }
        public long Insert(Message entity)
        {
            db.Messages.Add(entity);
            db.SaveChanges();
            return entity.ID;
        }
        public bool Update(Message entity)
        {
            try
            {
                var message = db.Messages.Find(entity.ID);
                message.Answer = entity.Answer;
                message.AnswerBy = entity.AnswerBy;
                message.AnswerDate = entity.AnswerDate;
                message.Status = true;
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
                var message = db.Messages.Find(id);
                db.Messages.Remove(message);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public IEnumerable<Message> ListAllPaging(int page, int pageSize)
        {
            return db.Messages.OrderByDescending(x => x.CreateDate).ToPagedList(page, pageSize);
        }
        public IEnumerable<Message> ListAllMessages()
        {
            var query = from tempUsers in db.Messages
                        select tempUsers;
            return query;
        }
        public Message GetbyID(long id)
        {
            return db.Messages.SingleOrDefault(x => x.ID == id);
        }
        public Message ViewDetail(long id)
        {
            return db.Messages.Find(id);
        }
    }
}
