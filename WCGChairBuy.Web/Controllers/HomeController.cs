using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WCGChairBuy.Web.Db;
using WCGChairBuy.Web.ViewModels;

namespace WCGChairBuy.Web.Controllers
{
    public class HomeController : Controller
    {
        /// <summary>
        /// 主页
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 登录页
        /// </summary>
        /// <returns></returns>
        public ActionResult Login()
        {
            return View(new LoginVModel());
        }
        /// <summary>
        /// 检查登录
        /// </summary>
        /// <returns></returns>
        public ActionResult CheckLogin(LoginVModel loginVModel)
        {
            return Redirect("Index");
        }

        /// <summary>
        /// 注册页面
        /// </summary>
        /// <returns></returns>
        public ActionResult Regist()
        {
            return View();
        }
        /// <summary>
        /// 注册页面
        /// </summary>
        /// <returns></returns>
        public ActionResult Register(RegistVModel registVModel)
        {
            if (ModelState.IsValid)
            {
                using (LsBuyEntities db = new LsBuyEntities())
                {
                    if (db.Users.Where(t => t.Email == registVModel.Email).Count() > 0)
                    {
                        //ModelState.
                    }
                    else
                    {
                        Db.User user = new Db.User
                        {
                            Id = Guid.NewGuid().ToString(),
                            Email = registVModel.Email,
                            Phone = registVModel.Phone,
                            UserType = 0,
                            Password = registVModel.Password,
                            UserName = registVModel.Email,
                            Sex = registVModel.Sex
                        };
                        db.Users.Add(user);
                        db.SaveChanges();
                    }
                    return Redirect("Index");
                }
            }
            else
            {
                return View("Regist", registVModel);
            }
            }
        }
    }