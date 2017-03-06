using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WCGChairBuy.Web.ViewModels
{
    /// <summary>
    /// 主页视图对象
    /// </summary>
    public class IndexVModel
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public IndexVModel()
        {
            ProductList = new List<ProductVModel>();
        }
        /// <summary>
        /// 头部大图产品 / 推广产品
        /// </summary>
        public ProductVModel HeadProduct { get; set; }
        /// <summary>
        /// 产品列表
        /// </summary>
        public List<ProductVModel> ProductList { get; set; }
    }
}