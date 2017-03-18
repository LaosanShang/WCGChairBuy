using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WCGChairBuy.Web.ViewModels
{
    public class ShoppingChartsVModel
    {
        public ShoppingChartsVModel()
        {
            ShoppingCharts = new List<ShoppingChartVModel>();
            Addresses = new List<AddressVModel>();
        }

        public List<ShoppingChartVModel> ShoppingCharts { get; set; }

        public List<AddressVModel> Addresses { get; set; }

        public string ValidMessage { get; set; }
    }
}