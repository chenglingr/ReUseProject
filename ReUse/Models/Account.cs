using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace ReUse.Models
{
    public class Account
    {
        [Key]
        public int ID { get; set; }
        [DisplayName("邮箱")]
        [Required(ErrorMessage = "请输入邮箱名")]
        public string Email { get; set; }
        [DisplayName("密码")]
        [Required(ErrorMessage = "请输入密码")]
        [StringLength(20, MinimumLength= 6, ErrorMessage = "密码在6和20个字之间")]
        public string PassWord { get; set; }
        [DisplayName("地址")]
        public string Address { get; set; }
        public virtual ICollection<Article> Articles { get; set; }
    }
}