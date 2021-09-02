using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Data.Entity.Validation;

namespace MyBlogWeb.Areas.Admin.Models
{
    public class User
    {
        public long ID { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 3)]
        [Display(Name = "Tài khoản")]
        public string UserName { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 3)]
        [Display(Name = "Họ và tên")]
        public string Name { get; set; }
        [Required]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}")]
        public string Email { get; set; }

        [Required]
        [RegularExpression(@"^[0][0-9]\d*$")]
        [Display(Name = "Số điện thoại")]
        public string Phone { get; set; }

        [Required]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,15}$")]
        [Display(Name = "Mật khẩu")]
        public string Password { get; set; }

        [NotMapped]
        [Required]
        [System.ComponentModel.DataAnnotations.Compare("Password")]
        [Display(Name = "Nhập lại mật khẩu")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "Trạng thái")]
        public bool Status { get; set; }
        public DateTime? CreateDate { get; set; }
        public string ModifiedBy { get; set; }
    }
}