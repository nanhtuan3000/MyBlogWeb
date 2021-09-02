using Model.Dao;
using Model.EF;
using MyBlogWeb.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyBlogWeb.Areas.Admin.Controllers
{
    public class MessageBoxController : BaseController
    {
        // GET: Admin/MessageBox
        public ActionResult Index(int page = 1, int pageSize = 1)
        {
            var dao = new MessageDao();
            var model = dao.ListAllPaging(page, pageSize);
            return View(model);
        }

        public ActionResult Edit(long id)
        {
            var dao = new MessageDao();
            var message = dao.ViewDetail(id);
            var message_model = new Models.MessageBoxModel();
            message_model.UserName = message.UserName;
            message_model.Detail = message.Detail;
            message_model.Answer = message.Answer;
            return View(message_model);
        }
        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Message message)
        {
            if (ModelState.IsValid)
            {
                var dao = new MessageDao();
                                
                var session = (UserLogin)Session[CommonConstants.USER_SESSION];
                message.AnswerBy = session.UserName;
                message.AnswerDate = DateTime.Now;
                bool result = dao.Update(message);
                if (result)
                {
                    return RedirectToAction("Index", "MessageBox");
                }
                else
                {
                    ModelState.AddModelError("", "Cập nhật trả lời không thành công");
                }

            }
            return View("Index");
        }
        [HttpPost]
        public ActionResult LoadData()
        {
            try
            {
                var dao = new MessageDao();
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
                var customerData = dao.ListAllMessages();
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
                        case "CreateDate":
                            if (sortColumnDir.Equals("desc"))
                            {
                                customerData = customerData.OrderByDescending(x => x.CreateDate + " " + sortColumnDir);
                            }
                            else
                            {
                                customerData = customerData.OrderBy(x => x.CreateDate + " " + sortColumnDir);
                            }
                            break;
                        case "AnswerDate":
                            if (sortColumnDir.Equals("desc"))
                            {
                                customerData = customerData.OrderByDescending(x => x.AnswerDate + " " + sortColumnDir);
                            }
                            else
                            {
                                customerData = customerData.OrderBy(x => x.AnswerDate + " " + sortColumnDir);
                            }
                            break;
                        case "AnswerBy":
                            if (sortColumnDir.Equals("desc"))
                            {
                                customerData = customerData.OrderByDescending(x => x.AnswerBy + " " + sortColumnDir);
                            }
                            else
                            {
                                customerData = customerData.OrderBy(x => x.AnswerBy + " " + sortColumnDir);
                            }
                            break;
                    }

                }
                //Search    
                if (!string.IsNullOrEmpty(searchValue))
                {
                    customerData = customerData.Where(m => m.UserName == searchValue);
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
        public JsonResult DeleteMessage(long id)
        {
            var result = new MessageDao().Delete(id);
            return Json(new
            {
                deleted = result
            });
        }

    }
}