using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.EF;
using PagedList;

namespace Model.Dao
{
    public class UserDao
    {
        MyBlogDbContext db = null;
        public UserDao()
            {
                db = new MyBlogDbContext();
            }
        public long Insert(User entity)
        {
            db.Users.Add(entity);
            db.SaveChanges();
            return entity.ID;
        }
        public bool Update(User entity)
        {
            try
            {
                var user = db.Users.Find(entity.ID);
                user.Name = entity.Name;
                user.Phone = entity.Phone;
                user.Email = entity.Email;
                if (!string.IsNullOrEmpty(entity.Password))
                {
                    user.Password = entity.Password;
                }
                
                user.ModifiedBy = entity.ModifiedBy;
                user.ModifiedDate = DateTime.Now;
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
                var user = db.Users.Find(id);
                db.Users.Remove(user);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public IEnumerable<User> ListAllPaging(int page, int pageSize)
        {
            return db.Users.OrderByDescending(x=>x.CreateDate).ToPagedList(page, pageSize);
        }
        public IEnumerable<User> ListAllUsers()
        {
            var query = from tempUsers in db.Users
                        select tempUsers;
            return query;
        }
        public User GetbyID(string UserName)
        {
            return db.Users.SingleOrDefault(x => x.UserName == UserName);
        }
        public User ViewDetail(long id)
        {
            return db.Users.Find(id);
        }
        public int Login(string UserName, string Password)
        {
            //var result = db.Users.Count(x => x.UserName == UserName && x.Password == Password);
            var result = db.Users.SingleOrDefault(x => x.UserName == UserName);
            if (result ==null)
            {
                return 0;   //tài khoản không tồn tại
            }
            else
            {
                if (result.Status == false)
                {
                    return -1;  //tài khoản bị khóa
                }
                else
                {
                    if (result.Password == Password)
                    {
                        return 1;
                    }
                    else
                    {
                        return -2;  //sai mật khẩu
                    }

                }

            }
        }
        public bool ChangeStatus(long id)
        {
            var user = db.Users.Find(id);
            user.Status = !user.Status;
            db.SaveChanges();
            return user.Status;
        }
    }
}
