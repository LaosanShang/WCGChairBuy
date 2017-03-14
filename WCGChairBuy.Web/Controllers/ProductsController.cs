using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WCGChairBuy.Web.Db;

namespace WCGChairBuy.Web.Controllers
{
    [Authorize]
    public class ProductsController : Controller
    {
        private LsBuyEntities db = new LsBuyEntities();

        /// <summary>
        /// 产品管理主页
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View(db.Products.ToList());
        }

        /// <summary>
        /// 产品详细信息
        /// </summary>
        /// <param name="id">产品Id</param>
        /// <returns></returns>
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        /// <summary>
        /// 新增产品
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// 添加产品
        /// </summary>
        /// <param name="product">产品信息</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ProductName,Price,UserId,CreatedTime,UpdatedTime,ModelNumber,Description,ImageUrl")] Product product)
        {
            if (ModelState.IsValid)
            {
                product.CreatedTime = DateTime.Now;
                product.UpdatedTime = DateTime.Now;
                product.UserId = User.Identity.Name;
                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(product);
        }

        // GET: Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        /// <summary>
        /// 编辑提交
        /// </summary>
        /// <param name="product">产品信息</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ProductName,Price,UserId,CreatedTime,UpdatedTime,ModelNumber,Description,ImageUrl")] Product product)
        {
            if (ModelState.IsValid)
            {
                Product p = db.Products.Where(t => t.Id == product.Id).FirstOrDefault();
                //修改赋值
                p.UpdatedTime = DateTime.Now;
                p.ProductName = product.ProductName;
                p.Price = product.Price;
                p.ModelNumber = product.ModelNumber;
                p.ImageUrl = product.ImageUrl;
                p.Description = product.Description;
                //标识为修改
                db.Entry(p).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(product);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        /// <summary>
        /// 删除提交
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
