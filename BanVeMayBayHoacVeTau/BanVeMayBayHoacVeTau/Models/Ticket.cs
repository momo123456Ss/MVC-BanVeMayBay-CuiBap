using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BanVeMayBayHoacVeTau.Models
{
    public class Ticket
    {
        HeThongBanVeTauHoacMayBayDataContext dbContext = new HeThongBanVeTauHoacMayBayDataContext();

        public int ProductID { get; set; }
        public string CustomerID { get; set; }
        public string EmployeeID { get; set; }
        public string ProductName { get; set; }
        public DateTime OrderDate  { get; set; }
        public DateTime Begin { get; set; }
        public bool Type { get; set; }
        public decimal? UnitPrice { get; set; }
        private int quanity;
        public decimal? Total { get { return UnitPrice * Quanity; } }

        public int Quanity { get => quanity; set => quanity = value; }
        public Ticket(int productID)
        {
            this.ProductID = productID;
            Product p = dbContext.Products.Single(n => n.ProductID == productID);
            OrderDate = DateTime.Now;
            Begin = DateTime.Now;
            ProductName = p.ProductName;
            UnitPrice = p.NormalPrice;
            Quanity = 1;
        }
       
    }
}