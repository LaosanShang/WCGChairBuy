using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WCGChairBuy.Web.ViewModels
{
    public class MessageVModel
    {
        public int Id { get; set; }
        [Display(Name = "留言")]
        [Required(ErrorMessage = "留言内容不能为空！")]
        public string Content { get; set; }
    }
}