using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyBlogWeb.Areas.Admin.Controllers
{
    public class UploadFilesController : BaseController
    {
        // GET: Admin/UploadFiles
        public ActionResult Index()
        {
            return View();
        }
    }
}