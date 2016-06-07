using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace ReUse.Models
{
    public class Goods
    {
        [Key]
        public int ID { get; set; }
        [DisplayName("物品名称")]
        [Required(ErrorMessage = "请输入物品名称")]
        public string GoodsName { get; set; }
        [DisplayName("物品图片")]
        public string Pic { get; set; }
        [DisplayName("所属分类-大类")]
        public string Style1 { get; set; }
        [DisplayName("所属分类-小类")]
        public string Style2 { get; set; }
        [DisplayName("商品介绍")]
        public string Description { get; set; }
        [DisplayName("新旧程度")]
        public string News { get; set; }//全新、 9成
        [DisplayName("交易类型")]
        public string ChangeType { get; set; }//交易类型：现金交易、积分交换、实物交换
        [DisplayName("交易价格")]
        public decimal ChangPrice { get; set; }//-1为面议  0为免费赠送  （现金交易、积分交换）价格>0
        [DisplayName("所需物品")]
        public string ChangeGoods { get; set; }
        [DisplayName("所在地区")]
        public string Area { get; set; }
        [DisplayName("交易地点")]
        public string Address { get; set; }
        [DisplayName("浏览次数")]
        public string ClickNum { get; set; }
        [DisplayName("发布时间")]
        public string CreatDate { get; set; }
        [DisplayName("状态")]
        public string State { get; set; }//0正常 1交易成功 2撤销
        public int UserID { get; set; }
        public virtual User User { get; set; }

    }
}