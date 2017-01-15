using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WCGChairBuy.Web.ViewModels
{
    /// <summary>
    /// 用户视图模型类
    /// </summary>
    public class UserVModel
    {
        [Display(Name = "邮箱")]
        [Required(ErrorMessage = "请输入注册的邮箱")]
        [EmailAddress(ErrorMessage = "邮箱格式输入不正确")]
        public string Email { get; set; }

        [Display(Name = "性别")]
        [Required(ErrorMessage = "请选择")]
        public int Sex { get; set; }

        [Display(Name = "联系电话")]
        [Required(ErrorMessage = "请输入联系电话")]
        public string Phone { get; set; }

    }
}