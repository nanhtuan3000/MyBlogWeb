using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model.Dao;
using BotDetect.Web.Mvc;

namespace MyBlogWeb.Controllers
{
    public class CustMessageController : Controller
    {
        // GET: CustMessage
        public ActionResult Index()
        {
            var dao = new MessageDao();
            var message = dao.ListAllMessages();
            return View(message);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        public ActionResult Detail(long id)
        {
            var dao = new MessageDao();
            var message = dao.GetbyID(id);
            return View(message);
        }

        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        [CaptchaValidationActionFilter("CaptchaCode", "CustMessageCaptcha", "Mã xác nhận không đúng!")]
        public ActionResult Create(Message CusMessage)
        {
            if (ModelState.IsValid)
            {
                var dao = new MessageDao();
                CusMessage.CreateDate = DateTime.Now;
                CusMessage.Status = false;
                long id = dao.Insert(CusMessage);
                if (id > 0)
                {
                    return RedirectToAction("Index", "CustMessage");
                }
                else
                {
                    ModelState.AddModelError("", "Gửi thư không thành công");
                    // Reset the captcha if your app's workflow continues with the same view
                    MvcCaptcha.ResetCaptcha("CustMessageCaptcha");
                }
               
            }
            return View();           
        }
    }
}