using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
namespace ReUse.Models
{
    //
    public class Tran
    {
        [DisplayName("编号")]
        public int ID { get; set; }
        [DisplayName("请求文字"), Required(ErrorMessage = "请输入请求文字")]
        public string Content { get; set; }
        [DisplayName("状态")]
        public int State { get; set; }//1请求状态 2接受请求 3 废弃 4交易成功 
        [DisplayName("创建时间")]
        public DateTime CreateDate { get; set; }
        [DisplayName("评价")]
        public int Star { get; set; }
        public int? UserID { get; set; }
        public int? GoodsID { get; set; }
        public virtual User User { get; set; }
        public virtual Goods Goods { get; set; }
    }
}