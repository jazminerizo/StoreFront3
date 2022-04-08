using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StoreFront3.UI.MVC.Models; //Added for access to CartItemViewModel class

namespace StoreFront3.UI.MVC.Controllers
{
    public class ShoppingCartController : Controller
    {
        // GET: ShoppingCart -- Generate this view with the List template for the cart item view model objects No data context
        public ActionResult Index()
        {
            //Pull the session cart into a local variable which we can then pass to the view

            var shoppingCart = (Dictionary<int, CartItemViewModel>)Session["cart"];

            if (shoppingCart == null || shoppingCart.Count == 0)
            {
                //User either hasn't put anything in the cart, removed all cart items, or session expired...
                //Set the cart to an wmpty cart obbject (we can still send that to the view)
                shoppingCart = new Dictionary<int, CartItemViewModel>();

                //Create a message informing them about the empty cart
                ViewBag.Message = "There are no items in your cart.";

            }

            else
            {
                ViewBag.Message = null; //Explicitely clears out that ViewBag variable
            }

            return View(shoppingCart);
        }

        public ActionResult RemoveFromCart(int id)
        {
            //Get the cart from the session and put into a local variable
            Dictionary<int, CartItemViewModel> shoppingCart = (Dictionary<int, CartItemViewModel>)Session["cart"];

            //Remove the item 
            shoppingCart.Remove(id);

            //Update the session
            Session["cart"] = shoppingCart;

            return RedirectToAction("Index");
        }

        public ActionResult UpdateCart(int productID, int qty)
        {
            //Get the cart from the Session and store it in a local variable
            Dictionary<int, CartItemViewModel> shoppingCart = (Dictionary<int, CartItemViewModel>)Session["cart"];

            //Target correct cart item using bookID for the key. Them change the Qty property with the qty parameter
            shoppingCart[productID].Qty = qty;

            //Return the (now updated) local cart to the session
            Session["cart"] = shoppingCart;

            //Send the user to the shopping Cart Index to see the update cart
            return RedirectToAction("Index");


        }

    }
}