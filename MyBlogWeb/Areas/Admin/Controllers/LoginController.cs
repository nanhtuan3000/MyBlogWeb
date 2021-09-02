using Model.Dao;
using MyBlogWeb.Areas.Admin.Models;
using MyBlogWeb.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyBlogWeb.Areas.Admin.Controllers
{
    public class LoginController : Controller
    {
        // GET: Admin/Login
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Login(LoginModel Model)
        {            
            if(ModelState.IsValid)
            {
                var dao = new UserDao();
                var result = dao.Login(Model.UserName, Encryptor.MD5Hash(Model.Password));
                if(result==1)
                {
                    var user = dao.GetbyID(Model.UserName);
                    var userSession = new UserLogin();
                    userSession.UserID = user.ID;
                    userSession.UserName = user.UserName;

                    Session.Add(CommonConstants.USER_SESSION, userSession);
                    return RedirectToAction("Index", "Home");
                }
                else if (result == 0)
                    {
                        ModelState.AddModelError("", "Tài khoản không tồn tại.");
                    }
                else if (result == -1)
                    {
                        ModelState.AddModelError("", "Tài khoản đang bị khóa.");
                    }
                else if (result == -2)
                    {
                        ModelState.AddModelError("", "Mật khẩu không đúng.");
                    }
                else
                    {
                        ModelState.AddModelError("", "Đăng nhập không đúng");
                    }
            }
            return View("Index");
        }
    }
}