using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WCGChairBuy.Web.ViewModels
{
    /// <summary>
    /// 个人中心视图模型类
    /// </summary>
    public class PersonalCenterVModel
    {
        public PersonalCenterVModel()
        {
            User = new UserVModel();
        }

        /// <summary>
        /// 用户信息
        /// </summary>
        public UserVModel User { get; set; }
    }
}