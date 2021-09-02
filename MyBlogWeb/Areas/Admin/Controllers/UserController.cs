using Model.Dao;
using Model.EF;
using MyBlogWeb.Common;
using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Linq.Dynamic;
//using MyBlog.Areas.Admin.Models

namespace MyBlogWeb.Areas.Admin.Controllers
{
    public class UserController : BaseController
    {
        // GET: Admin/User
        public ActionResult Index(int page = 1, int pageSize = 1)
        {
            var dao = new UserDao();
            var model = dao.ListAllPaging(page, pageSize);
            return View(model);
        }
        [HttpGet]
        public ActionResult Create()
        {            
            return View();
        }
        public ActionResult Edit(long id)
        {
            var dao = new UserDao();
            var user = dao.ViewDetail(id);
            var user_model = new Models.User();
            user_model.UserName = user.UserName;
            user_model.Email = user.Email;
            user_model.Phone = user.Phone;
            user_model.Status = user.Status;
            user_model.Name = user.Name;
            return View(user_model);
        }

        public ActionResult Logout()
        {
            Session[Common.CommonConstants.USER_SESSION] = null;
            return Redirect("/");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(User user)
        {
            if (ModelState.IsValid)
            {
                var dao = new UserDao();
                var check = dao.GetbyID(user.UserName);
                if (check == null)
                {
                    var EncryMd5Password = Encryptor.MD5Hash(user.Password);
                    user.Password = EncryMd5Password;
                    user.CreateDate = DateTime.Now;
                    long id = dao.Insert(user);
                    if (id > 0)
                    {
                        return RedirectToAction("Index", "User");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Thêm người dùng không thành công");
                    }
                }
                else
                {
                    ViewBag.error = "Tài khoản đã tồn tại.";
                    return View();

                }
            }
            return View("Index");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(User user)
        {
            if (ModelState.IsValid)
            {
                var dao = new UserDao();
                
                if (!string.IsNullOrEmpty(user.Password))
                {
                    var EncryMd5Password = Encryptor.MD5Hash(user.Password);
                    user.Password = EncryMd5Password;
                }
                var session = (UserLogin) Session[CommonConstants.USER_SESSION];
                user.ModifiedBy = session.UserName;
                bool result = dao.Update(user);
                if (result)
                {
                    return RedirectToAction("Index", "User");
                }
                else
                {
                    ModelState.AddModelError("", "Cập nhật người dùng không thành công");
                }
               
            }
            return View("Index");
        }
        [HttpPost]
        public ActionResult LoadData()
        {
            try
            {
                var dao = new UserDao();
                var draw = Request.Form.GetValues("draw").FirstOrDefault();
                var start = Request.Form.GetValues("start").FirstOrDefault();
                var length = Request.Form.GetValues("length").FirstOrDefault();
                var sortColumn = Request.Form.GetValues("columns[" + Request.Form.GetValues("order[0][column]").FirstOrDefault() + "][name]").FirstOrDefault();
                var sortColumnDir = Request.Form.GetValues("order[0][dir]").FirstOrDefault();
                var searchValue = Request.Form.GetValues("search[value]").FirstOrDefault();

                //Paging Size (10,20,50,100)    
                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;
                int recordsTotal = 0;

                // Getting all Customer data    
                var customerData = dao.ListAllUsers();
                //Sorting    
                if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDir)))
                {
                    switch (sortColumn)
                    {
                        case "UserName":
                            if (sortColumnDir.Equals("desc"))
                            {
                                customerData = customerData.OrderByDescending(x => x.UserName + " " + sortColumnDir);
                            }
                            else
                            {
                                customerData = customerData.OrderBy(x => x.UserName + " " + sortColumnDir);
                            }
                            break;
                        case "Name":
                            if (sortColumnDir.Equals("desc"))
                            {
                                customerData = customerData.OrderByDescending(x => x.Name + " " + sortColumnDir);
                            }
                            else
                            {
                                customerData = customerData.OrderBy(x => x.Name + " " + sortColumnDir);
                            }
                            break;
                        case "Phone":
                            if (sortColumnDir.Equals("desc"))
                            {
                                customerData = customerData.OrderByDescending(x => x.Phone + " " + sortColumnDir);
                            }
                            else
                            {
                                customerData = customerData.OrderBy(x => x.Phone + " " + sortColumnDir);
                            }
                            break;
                        case "Email":
                            if (sortColumnDir.Equals("desc"))
                            {
                                customerData = customerData.OrderByDescending(x => x.Email + " " + sortColumnDir);
                            }
                            else
                            {
                                customerData = customerData.OrderBy(x => x.Email + " " + sortColumnDir);
                            }
                            break;
                    }
                                    
                }
                //Search    
                if (!string.IsNullOrEmpty(searchValue))
                {
                    customerData = customerData.Where(m => m.Name == searchValue);
                }

                //total number of rows count     
                recordsTotal = customerData.Count();
                //Paging     
                var data = customerData.Skip(skip).Take(pageSize).ToList();
                
                //Returning Json Data    
                return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data });
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpPost]
        public JsonResult ChangeStatus(long id)
        {
            var result = new UserDao().ChangeStatus(id);
            return Json(new
            {
                status = result
            });
        }
        [HttpPost]
        public JsonResult DeleteUser(long id)
        {
            var result = new UserDao().Delete(id);
            return Json(new
            {
                deleted = result
            });
        }
    }
}