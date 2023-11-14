using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GioHanglt.Models;
using System.IO;

namespace GioHanglt.Controllers
{
    public class ShoppingCartController : Controller
    {
        DBSportStoreEntities database = new DBSportStoreEntities();
        // GET: ShoppingCart
        public ActionResult ShowCart()
        {
            if (Session["Cart"] == null) 
                return RedirectToAction("ShowCart", "ShoppingCart");
            Cart _cart = Session["Cart"] as Cart;
            return View(_cart); 
        }
        public Cart GetCart()
        {
            Cart cart = Session["Cart"] as Cart;
            if (cart == null || Session["Cart"] == null)
            {
                cart = new Cart();
                Session["Cart"] = cart;

            }
            return cart;
        }
        public ActionResult AddToCart(int id)
        {
            var _pro = database.Products.SingleOrDefault(s => s.ProductID == id);
            if (_pro != null)
            {
                GetCart().Add_Product_Cart(_pro);
            }
            return RedirectToAction("ShowCart", "ShoppingCart");
        }
        public ActionResult Checkout(FormCollection form)
        {
            try
            {
                Cart cart = Session["Cart"] as Cart;
                OrderPro _order = new OrderPro(); //Bang Hoa Don San pham 
                _order.DateOrder = DateTime.Now;
                _order.AddressDeliverry = form["AddressDelivery"];
                foreach (var item in cart.Items)
                {
                    OrderDetail _order_detail = new OrderDetail(); //Luu dong san pham vao bang Chi tiet Hoa don
                    _order_detail.IDOrder = _order.ID;
                    _order_detail.IDProduct = item._product.ProductID;
                    _order_detail.UnitPrice=(double) item._product.Price;
                    _order_detail.Quantity = item._quantity;
                    database.OrderDetails.Add(_order_detail);
                }
                database.SaveChanges();
                cart.ClearCart();
                return RedirectToAction("CheckOut Success", "ShoppingCart");
            }
            catch
            { return Content(""); }

        }
        
    }
}