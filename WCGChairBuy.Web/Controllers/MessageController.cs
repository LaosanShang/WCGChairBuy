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
    public class MessageController : Controller
    {
        /// <summary>
        /// 我要留言
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="msgVm"></param>
        /// <returns></returns>
        public ActionResult Add(MessageVModel msgVm)
        {
            using (Db.LsBuyEntities db =new Db.LsBuyEntities())
            {
                if (!ModelState.IsValid)
                    return View("Index",msgVm);
                //获取当前登录用户信息
                User user = db.Users.Where(t => t.UserName == User.Identity.Name).FirstOrDefault();
                db.Messages.Add(new Db.Message
                {
                    Content = msgVm.Content,
                    CreatedTime = DateTime.Now,
                    UserId = user.Id,
                    Createdby = user.UserName,
                });
                db.SaveChanges();
            }
            return RedirectToAction("Messages", "PersonalCenter");
        }
    }
}