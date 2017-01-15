using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WCGChairBuy.Web.Db;
using WCGChairBuy.Web.ViewModels;

namespace WCGChairBuy.Web.Controllers
{
    [Authorize]
    public class PersonalCenterController : Controller
    {
        /// <summary>
        /// 个人中心主页
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            using (LsBuyEntities db = new LsBuyEntities())
            {
                PersonalCenterVModel vm = new PersonalCenterVModel();
                User user = db.Users.Where(t => t.UserName == User.Identity.Name).FirstOrDefault();
                vm.User = new UserVModel { Sex = (int)user.Sex, Email = user.Email, Phone = user.Phone };
                return View(vm);
            }
        }

        /// <summary>
        /// 购物车
        /// </summary>
        /// <returns></returns>
        public ActionResult ShoppingCharts()
        {
            return View();
        }

        /// <summary>
        /// 收货地址
        /// </summary>
        /// <returns></returns>
        public ActionResult Addresses()
        {
            return View();
        }

        /// <summary>
        /// 我的留言
        /// </summary>
        /// <returns></returns>
        public ActionResult Messages()
        {
            return View();
        }
    }
}