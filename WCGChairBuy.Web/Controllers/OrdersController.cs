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
    public class OrdersController : Controller
    {
        /// <summary>
        /// 订单管理首页
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            using (LsBuyEntities db = new LsBuyEntities())
            {
                #region 订单列表
                List<OrderVModel> orders = new List<OrderVModel>();
                //根据用户Id获取订单列表
                orders = db.Database.SqlQuery<OrderVModel>(@"select o.OrderNo,p.ImageUrl,ad.Receiver,ad.Content Address,o.OrderStatus,p.Price,p.ProductName
from orders o
left join OrderDetails d on o.OrderNo = d.OrderNo
left join Addresses ad on ad.Id = o.AddressId
left join CustomerProducts cp on cp.Id = d.ProductId
left join Products p on cp.ProductId = p.Id ").ToList();

                //vm.Orders = orders;
                #endregion

                return View(orders);
            }
        }
    }
}