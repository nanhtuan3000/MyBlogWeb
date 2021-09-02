using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyBlogWeb.Areas.Admin.Models
{
    public class MessageBoxModel
    {
        [Display(Name = "Mật danh")]
        public string UserName { get; set; }

        [Display(Name = "Câu hỏi")]
        public string Detail { get; set; }

        [Required]
        [Display(Name = "Trả lời")]
        public string Answer { get; set; }
    }
}