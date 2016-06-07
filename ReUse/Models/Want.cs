using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Collections.Generic;

namespace ReUse.Models
{
    public class Want
    {
        [Key]
        public int ID { get; set; }
        [DisplayName("物品名称")]
        [Required(ErrorMessage = "请输入物品名称")]
        public string GoodsName { get; set; }
        [DisplayName("商品介绍")]
        public string Description { get; set; }
        [DisplayName("联系方式")]
        public string Tel { get; set; }
        [DisplayName("浏览次数")]
        public string ClickNum { get; set; }
        [DisplayName("发布时间")]
        public string CreatDate { get; set; }
        [DisplayName("状态")]
        public string State { get; set; }//0正常 1交易成功 2撤销
        public int? UserID { get; set; }
        public virtual User User { get; set; }
    }
}