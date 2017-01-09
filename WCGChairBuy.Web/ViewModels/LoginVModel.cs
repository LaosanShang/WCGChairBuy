using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WCGChairBuy.Web.ViewModels
{
    public class LoginVModel
    {
        [Display(Name= "用户名")]
        [Required(ErrorMessage = "请输入用户名")]
        public string UserName { get; set; }

        [Display(Name = "密码")]
        [Required(ErrorMessage = "请输入密码")]
        public string Password { get; set; }
    }
}