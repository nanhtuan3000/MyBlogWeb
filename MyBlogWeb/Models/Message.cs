using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MyBlogWeb.Models
{
    public class Message
    {
        public long ID { get; set; }

        [Required]
        [StringLength(250, MinimumLength = 3)]
        [Display(Name = "Mật danh")]
        public string UserName { get; set; }

        [Required]
        [Column(TypeName = "ntext")]
        [Display(Name = "Nội dung")]
        public string Detail { get; set; }

        [Display(Name = "Trạng thái")]
        public bool Status { get; set; }
                
        public DateTime? CreateDate { get; set; }

        public string CaptchaCode { get; set; }
    }
}