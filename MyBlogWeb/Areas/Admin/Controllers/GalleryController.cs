using Model.Dao;
using Model.EF;
using MyBlogWeb.Areas.Admin.Models;
using MyBlogWeb.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyBlogWeb.Areas.Admin.Controllers
{
    public class GalleryController : BaseController
    {
        // GET: Admin/Gallery
        public ActionResult Index(int page = 10, int pageSize = 1)
        {
            //var dao = new GalleryDao();
            //var model = dao.ListAllPaging(page, pageSize);
            //return View(model);
            return View();
        }

        public ActionResult Edit(long id)
        {
            var dao = new GalleryDao();
            var gallery = dao.ViewDetail(id);
            var gallery_model = new GalleryModel();
            gallery_model.Name = gallery.Name;
            gallery_model.Link = gallery.Link;
            gallery_model.ImageUrl = gallery.ImageUrl;            
            return View(gallery_model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveImage(Gallery item)
        {
            var result = "Successfully Added";
            if (item.Name != null && item.ImageUrl != null)
            {
                var dao = new GalleryDao();
                item.Status = true;
                var session = (UserLogin)Session[CommonConstants.USER_SESSION];
                item.CreateBy = session.UserName;
                item.CreateDate = DateTime.Now;
                long id = dao.Insert(item);
                if (id <= 0)                {

                    ModelState.AddModelError("", "Thêm ảnh lỗi!");
                    result = "Thêm ảnh lỗi!";                    
                }
            }
            else
            {
                ModelState.AddModelError("", "Thêm ảnh lỗi!");
                result = "Thêm ảnh lỗi!";
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult LoadData()
        {
            try
            {
                var dao = new GalleryDao();
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
                var customerData = dao.ListAllGallery();
                //Sorting    
                if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDir)))
                {
                    switch (sortColumn)
                    {
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
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Gallery gallery)
        {
            if (ModelState.IsValid)
            {
                var dao = new GalleryDao();
                
                var session = (UserLogin)Session[CommonConstants.USER_SESSION];
                gallery.ModifiedBy = session.UserName;
                bool result = dao.Update(gallery);
                if (result)
                {
                    return RedirectToAction("Index", "Gallery");
                }
                else
                {
                    ModelState.AddModelError("", "Cập nhật ảnh không thành công");
                }

            }
            return View("Index");
        }

        [HttpPost]
        public JsonResult ChangeStatus(long id)
        {
            var result = new GalleryDao().ChangeStatus(id);
            return Json(new
            {
                status = result
            });
        }
        [HttpPost]
        public JsonResult DeleteGallery(long id)
        {
            var result = new GalleryDao().Delete(id);
            return Json(new
            {
                deleted = result
            });
        }
    }
}