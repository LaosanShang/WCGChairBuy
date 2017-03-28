using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
                #region 用户信息
                PersonalCenterVModel vm = new PersonalCenterVModel();
                User user = db.Users.Where(t => t.UserName == User.Identity.Name).FirstOrDefault();
                vm.User = new UserVModel { Sex = (int)user.Sex, Email = user.Email, Phone = user.Phone };
                #endregion

                #region 订单列表
                List<OrderVModel> orders = new List<OrderVModel>();
                //根据用户Id获取订单列表
                orders = db.Database.SqlQuery<OrderVModel>(@"select o.OrderNo,p.ImageUrl,ad.Receiver,ad.Content Address,o.OrderStatus,p.Price,p.ProductName
from orders o
left join OrderDetails d on o.OrderNo = d.OrderNo
left join Addresses ad on ad.Id = o.AddressId
left join CustomerProducts cp on cp.Id = d.ProductId
left join Products p on cp.ProductId = p.Id
where o.UserId = @userId", new SqlParameter("userId", user.Id)).ToList();

                vm.Orders = orders;
                #endregion

                return View(vm);
            }
        }

        /// <summary>
        /// 购物车
        /// </summary>
        /// <returns></returns>
        public ActionResult ShoppingCharts()
        {
            using (LsBuyEntities db = new LsBuyEntities())
            {
                User user = db.Users.Where(t => t.UserName == User.Identity.Name).FirstOrDefault();
                List<ShoppingChartVModel> spcharts = db.Database.SqlQuery<ShoppingChartVModel>(@"select t1.Id,t1.ProductId CustomerProductId,t2.Image,
t2.Color,t2.Text,t2.ProductId,
t3.Price,t3.ProductName,t3.ModelNumber,t3.ImageUrl
from ShoppingCharts t1
left join CustomerProducts t2 on t1.ProductId= t2.Id
left join Products t3 on t2.ProductId = t3.Id
where t1.UserId = @userId", new SqlParameter("userId", user.Id)).ToList();
                List<AddressVModel> addresses = db.Database.SqlQuery<AddressVModel>(@"select * from Addresses where UserId=@userId", new SqlParameter("userId", user.Id)).ToList();
                return View(new ShoppingChartsVModel
                {
                    Addresses = addresses,
                    ShoppingCharts = spcharts
                });
            }
        }
        /// <summary>
        /// 提交订单
        /// </summary>
        /// <returns></returns>
        public ActionResult ConfirmOrder(int[] id, string AddressId)
        {
            if(id==null||id.Length <1||string.IsNullOrEmpty(AddressId))
            {
                using (LsBuyEntities db = new LsBuyEntities())
                {
                    User user = db.Users.Where(t => t.UserName == User.Identity.Name).FirstOrDefault();
                    List<ShoppingChartVModel> spcharts = db.Database.SqlQuery<ShoppingChartVModel>(@"select t1.Id,t1.ProductId CustomerProductId,t2.Image,
t2.Color,t2.Text,t2.ProductId,
t3.Price,t3.ProductName,t3.ModelNumber,t3.ImageUrl
from ShoppingCharts t1
left join CustomerProducts t2 on t1.ProductId= t2.Id
left join Products t3 on t2.ProductId = t3.Id
where t1.UserId = @userId", new SqlParameter("userId", user.Id)).ToList();
                    List<AddressVModel> addresses = db.Database.SqlQuery<AddressVModel>(@"select * from Addresses where UserId=@userId", new SqlParameter("userId", user.Id)).ToList();
                    ModelState.AddModelError("ValidMessage", "请选择收货地址或商品！");
                    return View("ShoppingCharts", new ShoppingChartsVModel
                    {
                        Addresses = addresses,
                        ShoppingCharts = spcharts
                    });
                }
                
            }
            using (LsBuyEntities db = new LsBuyEntities())
            {
                User user = db.Users.Where(t => t.UserName == User.Identity.Name).FirstOrDefault();
                #region 生成订单
                string orderNo = DateTime.Now.ToString("yyyyMMddHHmmss") + user.Id.ToString().PadLeft(5, '0');
                Order order = new Order
                {
                    AddressId = AddressId,
                    OrderStatus = 0,
                    UserId = user.Id,
                    CreatedTime = DateTime.Now,
                    OrderNo = orderNo
                };
                db.Orders.Add(order);
                //订单详情
                db.ShoppingCharts.Where(t => id.Contains(t.Id)).ToList()
                    .ForEach(t =>
                    {
                        OrderDetail detail = new OrderDetail
                        {
                            OrderNo = orderNo,
                            ProductId = t.ProductId.ToString()
                        };
                        db.OrderDetails.Add(detail);
                    });
                //保存
                db.SaveChanges();
                #endregion
                return RedirectToAction("ConfirmOrderSuccess");
            }
        }
        /// <summary>
        /// 提交订单成功
        /// </summary>
        /// <returns></returns>
        public ActionResult ConfirmOrderSuccess()
        {
            return View();
        }

        #region 收货地址
        /// <summary>
        /// 收货地址
        /// </summary>
        /// <returns></returns>
        public ActionResult Addresses()
        {
            using (LsBuyEntities db = new LsBuyEntities())
            {
                List<AddressVModel> adds = new List<AddressVModel>();
                User user = db.Users.Where(t => t.UserName == User.Identity.Name).FirstOrDefault();
                db.Addresses.Where(t => t.UserId == user.Id).ToList()
                    .ForEach(t =>
                    {
                        adds.Add(new AddressVModel
                        {
                            Id = t.Id,
                            Content = t.Content,
                            Reveiver = t.Receiver,
                            Phone = t.Phone
                        });
                    });
                return View(adds);
            }

        }
        /// <summary>
        /// 添加收货地址
        /// </summary>
        /// <returns></returns>
        public ActionResult AddAddress()
        {
            return View();
        }
        /// <summary>
        /// 修改收货地址
        /// </summary>
        /// <returns></returns>
        public ActionResult ModifyAddress(int id)
        {
            using (LsBuyEntities db = new LsBuyEntities())
            {
                Address ads = db.Addresses.Where(t => t.Id == id).FirstOrDefault();
                AddressVModel vm = new AddressVModel
                {
                    Id = ads.Id,
                    Content = ads.Content,
                    Phone = ads.Phone,
                    Reveiver = ads.Receiver
                };
                return View(vm);
            }
        }
        /// <summary>
        /// 添加收货地址
        /// </summary>
        /// <param name="vModel"></param>
        /// <returns></returns>
        public ActionResult ConfirmAddAddress(AddressVModel vModel)
        {
            if (ModelState.IsValid)
            {
                using (LsBuyEntities db = new LsBuyEntities())
                {
                    User user = db.Users.Where(t => t.UserName == User.Identity.Name).FirstOrDefault();
                    Address address = new Address()
                    {
                        Content = vModel.Content,
                        Phone = vModel.Phone,
                        Receiver = vModel.Reveiver,
                        UserId = user.Id,
                        CreatedTime = DateTime.Now,
                        UpdatedTime = DateTime.Now
                    };
                    db.Addresses.Add(address);
                    db.SaveChanges();
                }
                return Redirect("Addresses");
            }
            else
            {
                return View("AddAddress", vModel);
            }

        }
        /// <summary>
        /// 添加收货地址
        /// </summary>
        /// <param name="vModel"></param>
        /// <returns></returns>
        public ActionResult ConfirmModifyAddress(AddressVModel vModel)
        {
            if (ModelState.IsValid)
            {
                using (LsBuyEntities db = new LsBuyEntities())
                {
                    Address address = db.Addresses.Where(t => t.Id == vModel.Id).FirstOrDefault();
                    address.Content = vModel.Content;
                    address.Phone = vModel.Phone;
                    address.Receiver = vModel.Reveiver;
                    address.UpdatedTime = DateTime.Now;
                    db.SaveChanges();
                }
                return Redirect("Addresses");
            }
            else
            {
                return View("ModifyAddress", vModel);
            }

        }
        /// <summary>
        /// 删除收货地址
        /// </summary>
        /// <param name="id">收货地址Id</param>
        /// <returns></returns>
        public ActionResult DeleteAddress(int id)
        {
            using (LsBuyEntities db = new LsBuyEntities())
            {
                var address = db.Addresses.Where(t => t.Id == id).FirstOrDefault();
                db.Addresses.Remove(address);
                db.SaveChanges();
            }
            return Redirect("Addresses");

        }
        #endregion

        /// <summary>
        /// 我的留言
        /// </summary>
        /// <returns></returns>
        public ActionResult Messages()
        {
            using (Db.LsBuyEntities db = new Db.LsBuyEntities())
            {
                var messages = db.Messages.AsNoTracking().Where(t => t.Createdby == User.Identity.Name)
                    .ToList();
                return View(messages);
            }
        }
    }
}