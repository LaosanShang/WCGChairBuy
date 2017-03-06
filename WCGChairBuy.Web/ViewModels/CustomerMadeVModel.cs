using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WCGChairBuy.Web.ViewModels
{
    public class CustomerMadeVModel
    {
        /// <summary>
        /// 产品
        /// </summary>
        public ProductVModel Product { get; set; }
        /// <summary>
        /// 定制产品
        /// </summary>
        public CustomerProductVModel CustomerProduct { get; set; }
    }
}