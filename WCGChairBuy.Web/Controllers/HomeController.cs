using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
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
            using (LsBuyEntities db = new LsBuyEntities())
            {
                IndexVModel indexVm = new IndexVModel();
                List<Product> products = db.Products.ToList();
                products.ForEach(t => indexVm.ProductList.Add(new ProductVModel
                {
                    Id = t.Id,
                    ProductName = t.ProductName,
                    ModelNumber = t.ModelNumber,
                    ImageUrl = t.ImageUrl,
                    Price = t.Price,
                    Description = t.Description
                }));

                return View(indexVm);
            }
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
            //初始化数据访问上下午对象
            // using(...){ ...} 的作用 在‘}’结束释放 LsBuyEntities db = new LsBuyEntities() 对象
            using (LsBuyEntities db = new LsBuyEntities())
            {
                //0.执行模型验证用户名或密码是否存在
                if (!ModelState.IsValid) return View("Login", loginVModel);
                else
                {
                    //1.根据用户名和密码到数据库中查询用户表 
                    var user = db.Users.Where(t => t.UserName == loginVModel.UserName)
                        .Where(t => t.Password == loginVModel.Password)
                        .FirstOrDefault();
                    //2.判断是否存在符合条件的用户
                    if (user == null)
                    {
                        //3.假如没有符合条件的用户  那么返回“用户名或密码”的错误到页面
                        ModelState.AddModelError("UserName", "用户名或密码错误");
                        return View("Login", loginVModel);
                    }
                    //4.加入用户存在，那么执行登录操作。这里使用Form验证
                    FormsAuthentication.SetAuthCookie(loginVModel.UserName, false);
                    //5.登录成功后跳转到首页
                    return Redirect("Index");
                }
            }//通过using关键字释放 db 对象

        }
        /// <summary>
        /// 退出登录
        /// </summary>
        /// <returns></returns>
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
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
                        ModelState.AddModelError("Email", "邮箱已注册！");
                        return View("Regist", registVModel);
                    }
                    else
                    {
                        Db.User user = new Db.User
                        {
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
        /// <summary>
        /// 关于我们
        /// </summary>
        /// <returns></returns>
        public ActionResult About()
        {
            return View();
        }
    }
}