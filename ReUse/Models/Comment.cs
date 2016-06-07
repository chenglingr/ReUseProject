using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
namespace ReUse.Models
{
    public class Comment
    {
        [Key]
        public int ID { get; set; }
        [DisplayName("评论内容")]
        [Required(ErrorMessage = "请输入评论内容")]
        public string Content { get; set; }
        [DisplayName("评论时间")]
        public string CeateDate { get; set; }
        public int? UserID { get; set; }
        public int? GoodsID { get; set; }
        public virtual User User { get; set; }
        public virtual Goods Goods { get; set; }
    }
}