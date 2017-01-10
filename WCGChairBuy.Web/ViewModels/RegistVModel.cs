using System.ComponentModel.DataAnnotations;

namespace WCGChairBuy.Web.ViewModels
{
    /// <summary>
    /// 注册视图模型类
    /// </summary>
    public class RegistVModel
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

        [Required(ErrorMessage = "请输入密码")]
        [StringLength(100, ErrorMessage = "{0} 必须至少包含 {2} 个字符。", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "密码")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "确认密码")]
        [Compare("Password", ErrorMessage = "密码和确认密码不匹配。")]
        public string ConfirmPassword { get; set; }
    }
}