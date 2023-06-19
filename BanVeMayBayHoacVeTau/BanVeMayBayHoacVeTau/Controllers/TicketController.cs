using BanVeMayBayHoacVeTau.Library;
using BanVeMayBayHoacVeTau.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.Mvc;

namespace BanVeMayBayHoacVeTau.Controllers
{
    public class TicketController : BaseController
    {
        // GET: Ticket
        HeThongBanVeTauHoacMayBayDataContext dbContext = new HeThongBanVeTauHoacMayBayDataContext();
        bool checkCus = false;
        public List<Ticket> GetListCarts()
        {

            List<Ticket> tickets = Session["Ticket"] as List<Ticket>;
            if (tickets == null)
            {
                tickets = new List<Ticket>();
                Session["Ticket"] = tickets;
            }
            return tickets;
        }
        private int Count()
        {
            int n = 0;
            List<Ticket> tickets = Session["Ticket"] as List<Ticket>;
            if (tickets != null)
            {
                n = tickets.Sum(s => s.Quanity);

            }
            return n;
        }
        private decimal Total()
        {
            decimal? total = 0;
            List<Ticket> tickets = Session["Ticket"] as List<Ticket>;
            if (tickets != null)
            {
                total = tickets.Sum(s => s.Total);

            }
            return (decimal)total;

        }
        public ActionResult AddTicket(int id)
        {
            List<Ticket> tickets = GetListCarts();
            Ticket t = tickets.Find(s => s.ProductID == id);
            if (t == null)
            {
                t = new Ticket(id);
                var check = dbContext.Customers.Where(m => m.CustomerID == Session["User"]).FirstOrDefault();
                if (check != null)
                {
                    t.CustomerID = (string)Session["User"];
                    //t.CustomerID = MyString.userTMP;
                }
                else
                {
                    t.EmployeeID = (string)Session["User"];
                }
                tickets.Add(t);
            }
            else
            {
                t.Quanity++;
            }
            ViewBag.CountProduct = tickets.Sum(s => s.Quanity);
            ViewBag.Total = tickets.Sum(s => s.Total);
            return RedirectToAction("ListTickets");
        }
        public ActionResult ListTickets()
        {
            List<Ticket> tickets = GetListCarts();

            ViewBag.CountProduct = tickets.Sum(s => s.Quanity);
            ViewBag.Total = tickets.Sum(s => s.Total);
            return View(tickets);
        }
        public ActionResult Edit(int id)
        {
            List<Ticket> tickets = GetListCarts();
            Ticket t = tickets.Find(s => s.ProductID == id);
            
            return View(t);
        }
        [HttpPost]
        public ActionResult Edit(int id, FormCollection form)
        {
            try
            {
                List<Ticket> tickets = GetListCarts();
                var t = tickets.Where(s => s.ProductID == id).FirstOrDefault();
                UpdateModel(t);
                Session["User"] = MyString.ToMD5(t.CustomerID);
                Session["Cus"] = MyString.ToMD5(t.CustomerID);

                return RedirectToAction("ListTickets");

            }
            catch
            {
                return RedirectToAction("Edit");
            }
            
        }
        public ActionResult Delete(int id)
        {
            List<Ticket> carts = GetListCarts();
            Ticket c = carts.Find(s => s.ProductID == id);
            carts.Remove(c);
            return RedirectToAction("ListTickets");

        }
        public ActionResult CreateTicket()
        {
            return View();
        }
        public ActionResult OrderProduct(FormCollection f)
        {
            var rowCus = dbContext.Customers.Where(m => m.CustomerID == Session["User"]).FirstOrDefault();
            var rowEmp = dbContext.Employees.Where(m => m.EmployeeID == Session["User"]).FirstOrDefault();
            using (TransactionScope tran = new TransactionScope())
            {
                try
                {
                    List<Ticket> tickets;
                    //1.Tao moi don hang
                    Order order = new Order();
                    if (rowCus != null)
                    {
                        order.CustomerID = (string)Session["User"];
                    }
                    else if (rowEmp != null)
                    {
                        order.EmployeeID = (string)Session["User"];
                        order.CustomerID = (string)Session["Cus"];
                    }
                    order.OrderDate = DateTime.Now;
                    order.Begin = DateTime.Now;
                    dbContext.Orders.InsertOnSubmit(order);
                    dbContext.SubmitChanges();
                    
                    tickets = GetListCarts();
                    foreach (Ticket item in tickets)
                    {
                        //Tao 1 orderDetails
                        Order_Detail d = new Order_Detail();
                        d.OrderID = order.OrderID;
                        d.ProductID = item.ProductID;
                        d.Price = item.UnitPrice;
                        d.Quanity = (short?)item.Quanity;
                        d.Discount = 0;
                        //Them SP vao OrderDetail
                        dbContext.Order_Details.InsertOnSubmit(d);
                    }
                    dbContext.SubmitChanges();
                    //lam rong session              
                    tran.Complete();
                    Session["Ticket"] = null;
                }
                catch (Exception)
                {
                    tran.Dispose();
                    RedirectToAction("ListCarts");
                }
            }

            return Content("<script language='javascript' type='text/javascript'>alert('Đã đặt vé!');window.location = '/Ticket/ListTickets'</script>");
        }

    }
}