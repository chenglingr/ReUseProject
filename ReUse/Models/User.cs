using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
namespace ReUse.Models
{
    public class User
    {
        [Key]
        public int ID { get; set; }
        [DisplayName("用户名")]
        [Required(ErrorMessage = "请输入用户名")]
        public string UserName { get; set; }
        [DisplayName("密码")]
        [Required(ErrorMessage = "请输入密码")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "密码在6和20个字之间")]
        public string UserPwd { get; set; }
        [DisplayName("电子邮箱")]
        public string Email { get; set; }
        [DisplayName("真实姓名")]
        public string RealName { get; set; }
        [DisplayName("手机号")]
        public string Tel { get; set; }
        [DisplayName("微信号")]
        public string WeiXin { get; set; }
        [DisplayName("QQ号")]
        public string QQ { get; set; }
        [DisplayName("积分")]
        public int Score { get; set; }//1积分估值1元
        [DisplayName("状态")]
        public int State { get; set; }//1正常 2禁用  3消失
        public virtual ICollection<Goods> Goodss { get; set; }
    }
}