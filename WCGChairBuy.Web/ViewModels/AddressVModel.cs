using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WCGChairBuy.Web.ViewModels
{
    public class AddressVModel
    {
        public int Id { get; set; }
        /// <summary>
        /// 内容
        /// </summary>
        [Display(Name = "收货地址")]
        [Required(ErrorMessage = "请填写收货地址")]
        public string Content { get; set; }
        [Display(Name = "联系电话")]
        [Required(ErrorMessage = "请填写收货人联系电话")]
        public string Phone { get; set; }
        [Display(Name = "收货人")]
        [Required(ErrorMessage = "请填写收货人姓名")]
        public string Reveiver { get; set; }
    }
}