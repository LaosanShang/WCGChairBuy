using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
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
                product.ImageUrl = SaveFile();
                product.CreatedTime = DateTime.Now;
                product.UpdatedTime = DateTime.Now;
                product.UserId = User.Identity.Name;
                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(product);
        }
        private string SaveFile()
        {
            string info = string.Empty;
            try
            {
                //获取客户端上传的文件集合
                HttpFileCollection files = System.Web.HttpContext.Current.Request.Files;
                //判断是否存在文件
                if (files.Count > 0)
                {
                    //获取文件集合中的第一个文件(每次只上传一个文件)
                    HttpPostedFile file = files[0];
                    //定义文件存放的目标路径
                    string targetDir = System.Web.HttpContext.Current.Server.MapPath("~/Images/");
                    //创建目标路径
                    Directory.CreateDirectory(targetDir);
                    //组合成文件的完整路径
                    string path = System.IO.Path.Combine(targetDir, System.IO.Path.GetFileName(file.FileName));
                    //保存上传的文件到指定路径中
                    file.SaveAs(path);
                    info = "~/Images/" + file.FileName;
                }
                else
                    throw new ApplicationException("请选择文件");
            }
            catch
            {
                info = "";
            }
            return info;
        }

        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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
