using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WCGChairBuy.Web.ViewModels
{
    public class OrderVModel
    {
        /// <summary>
        /// 订单号
        /// </summary>
        public string OrderNo { get; set; }
        /// <summary>
        /// 收货地址
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// 收件人
        /// </summary>
        public string Receiver { get; set; }
        /// <summary>
        /// 订单状态
        /// </summary>
        public int OrderStatus { get; set; }
        /// <summary>
        /// 商品名称
        /// </summary>
        public string ProductName { get; set; }
        /// <summary>
        /// 商品地址
        /// </summary>
        public string ImageUrl { get; set; }

        public string OrderStatusName
        {
            get
            {
                string _orderStatusName = "已提交" ;
                switch(OrderStatus)
                {
                    case 0:
                        _orderStatusName = "已提交";
                        break;
                    case 1:
                        _orderStatusName = "已确认";
                        break;
                    default:
                        break;
                };
                return _orderStatusName;
            }
        }
        /// <summary>
        /// 商品列表
        /// </summary>
        //public List<CustomerProductVModel> Products { get; set; }
    }
}