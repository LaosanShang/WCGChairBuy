using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WCGChairBuy.Web.Db;
using WCGChairBuy.Web.ViewModels;

namespace WCGChairBuy.Web.Controllers
{
    /// <summary>
    /// 定制
    /// </summary>
    [Authorize]
    public class CustomerMadeController : Controller
    {
        /// <summary>
        /// 定制页面主页
        /// </summary>
        /// <returns></returns>
        public ActionResult Index(int pid)
        {
            using (LsBuyEntities db = new LsBuyEntities())
            {
                CustomerMadeVModel vm = new CustomerMadeVModel();
                Product product = db.Products.Where(t => t.Id == pid).FirstOrDefault();
                vm.Product = new ProductVModel {
                    Id = product.Id,
                    Price = product.Price,
                    ImageUrl = product.ImageUrl,
                    Description = product.Description,
                    ModelNumber = product.ModelNumber,
                    ProductName = product.ProductName
                };
                vm.CustomerProduct = new CustomerProductVModel
                {
                    ProductId = product.Id,
                    Color = "red",
                    Image = "img1",
                    Text = "",
                };
                return View(vm);
            }
        }
    }
}