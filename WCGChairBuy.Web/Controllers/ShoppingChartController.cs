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
    [Authorize]
    public class ShoppingChartController : Controller
    {
        /// <summary>
        /// 首页
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 添加到购物车
        /// </summary>
        /// <param name="cvm"></param>
        /// <returns></returns>
        public ActionResult Add(CustomerMadeVModel cvm)
        {
            try
            {
                using (LsBuyEntities db = new LsBuyEntities())
                {
                    //获取当前登录用户信息
                    User user = db.Users.Where(t => t.UserName == User.Identity.Name).FirstOrDefault();
                    CustomerProduct cutomerPro =
                        db.CustomerProducts.Where(t =>
                        t.Color == cvm.CustomerProduct.Color &&
                    t.Text == cvm.CustomerProduct.Text &&
                    t.Image == cvm.CustomerProduct.Image &&
                    t.ProductId == cvm.CustomerProduct.ProductId).FirstOrDefault();
                    if (cutomerPro == null)
                    {
                        CustomerProduct customerPro = new CustomerProduct
                        {
                            Color = cvm.CustomerProduct.Color,
                            Image = cvm.CustomerProduct.Image,
                            Text = cvm.CustomerProduct.Text,
                            ProductId = cvm.CustomerProduct.ProductId,
                        };
                        //添加定制产品
                        db.CustomerProducts.Add(customerPro);
                        db.SaveChanges();
                        //查询定制产品
                        cutomerPro =
                        db.CustomerProducts.Where(t =>
                        t.Color == cvm.CustomerProduct.Color &&
                    t.Text == cvm.CustomerProduct.Text &&
                    t.Image == cvm.CustomerProduct.Image &&
                    t.ProductId == cvm.CustomerProduct.ProductId).FirstOrDefault();
                    }

                    //加入购物车
                    ShoppingChart shoppingChart = new ShoppingChart
                    {
                        ProductId = cutomerPro.Id,
                        UserId = user.Id,
                        CreatedTime = DateTime.Now
                    };
                    //加入购物车
                    db.ShoppingCharts.Add(shoppingChart);
                    //保存到数据库
                    db.SaveChanges();

                    return Redirect("AddSuccess");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 添加到购物车-首页添加
        /// </summary>
        /// <param name="cvm"></param>
        /// <returns></returns>
        public ActionResult AddWithDefaultProduct(int productId)
        {
            try
            {
                using (LsBuyEntities db = new LsBuyEntities())
                {
                    string defaultColor = "red", defaultImage = "img1", defaultText = "";
                    //获取当前登录用户信息
                    User user = db.Users.Where(t => t.UserName == User.Identity.Name).FirstOrDefault();
                    CustomerProduct cutomerPro =
                        db.CustomerProducts.Where(t =>
                        t.Color == defaultColor &&
                    t.Text == defaultText &&
                    t.Image == defaultImage &&
                    t.ProductId == productId).FirstOrDefault();
                    if (cutomerPro == null)
                    {
                        CustomerProduct customerPro = new CustomerProduct
                        {
                            Color = defaultColor,
                            Image = defaultImage,
                            Text = defaultText,
                            ProductId = productId,
                        };
                        //添加定制产品
                        db.CustomerProducts.Add(customerPro);
                        db.SaveChanges();
                        //查询定制产品
                        cutomerPro =
                        db.CustomerProducts.Where(t =>
                        t.Color == defaultColor &&
                    t.Text == defaultText &&
                    t.Image == defaultImage &&
                    t.ProductId == productId).FirstOrDefault();
                    }

                    //加入购物车
                    ShoppingChart shoppingChart = new ShoppingChart
                    {
                        ProductId = cutomerPro.Id,
                        UserId = user.Id,
                        CreatedTime = DateTime.Now
                    };
                    //加入购物车
                    db.ShoppingCharts.Add(shoppingChart);
                    //保存到数据库
                    db.SaveChanges();

                    return Redirect("AddSuccess");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 添加成功
        /// </summary>
        /// <returns></returns>
        public ActionResult AddSuccess()
        {
            return View();
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Remove(int id)
        {
            using (LsBuyEntities db = new LsBuyEntities())
            {
                var shopchart= db.ShoppingCharts.Where(t => t.Id == id).FirstOrDefault();
                db.ShoppingCharts.Remove(shopchart);
                db.SaveChanges();
                return RedirectToAction("ShoppingCharts", "PersonalCenter");
            }
        }
    }
}