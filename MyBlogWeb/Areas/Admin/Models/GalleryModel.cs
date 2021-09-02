using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyBlogWeb.Areas.Admin.Models
{
    public class GalleryModel
    {
        public long ID { get; set; }

        [Required(ErrorMessage = "Nhập tên ảnh")]
        [Display(Name = "Tên ảnh")]
        public string Name { get; set; }

        [Display(Name = "Link mở")]
        public string Link { get; set; }

        [Required(ErrorMessage = "Chọn đường dẫn file ảnh")]
        [Display(Name = "Đường dẫn")]
        public string ImageUrl { get; set; }

        [Display(Name = "Trạng thái")]
        public bool Status { get; set; }
    }
}