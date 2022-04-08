using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Drawing; //
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using StoreFront3.DATA.EF;
using StoreFront3.UI.MVC.Utilities; 
using StoreFront3.UI.MVC.Models; //Added for access to CartItemViewModel


namespace StoreFront3.UI.MVC.Controllers
{
    public class ProductsController : Controller
    {
        private FireScaleDragonsEntities db = new FireScaleDragonsEntities();

        // GET: Products
        public ActionResult Index()
        {
            var products = db.Products.Include(p => p.Category).Include(p => p.StockStatus);
            return View(products.ToList());
        }

        // GET: Products/Details/5
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

        #region Custom Add-to-Cart Functionality (Called from Details View)

        public ActionResult AddToCart(int qty, int productID)
        {
            //Create an empty shell for the LOCAL shopping cart variable
            Dictionary<int, CartItemViewModel> shoppingCart = null;

            //Check if the Session shopping cart exists. If so, use it to populate the local version
            if (Session["cart"] != null)
            {
                //Session shopping cart exists. Put its items in the local version, which is easier to work with
                shoppingCart = (Dictionary<int, CartItemViewModel>)Session["cart"];
                //We need to UNBOX the Session object to its smaller, more specific type -- Explicit casting
            }
            else
            {
                //If the Session cart doesn't exist yet, we need to instantiate it to get started
                shoppingCart = new Dictionary<int, CartItemViewModel>();
            }//After this if/else, we now have a local cart that's ready to add things to it

            //Find the product they referenced by its ID
            Product product = db.Products.Where(b => b.ProductID == productID).FirstOrDefault();

            if (product == null)
            {
                //If given a bad ID, return the user to some other page to try again.
                //Alternatively, we could throw some kind of error, which we will 
                //discuss further in Module 6.
                return RedirectToAction("Index");
            }
            else
            {
                //If the Book IS valid, add the line-item to the cart
                CartItemViewModel item = new CartItemViewModel(qty, product);

                //Put the item in the local cart. If they already have that product as a
                //cart item, the instead we will update the quantity. This is a big part
                //of why we have the dictionary.
                if (shoppingCart.ContainsKey(product.ProductID))
                {
                    shoppingCart[product.ProductID].Qty += qty;
                }
                else
                {
                    shoppingCart.Add(product.ProductID, item);
                }

                //Now update the SESSION version of the cart so we can maintain that info between requests
                Session["cart"] = shoppingCart; //No explicit casting needed here

            }

            //Send them to View their Cart Items
            return RedirectToAction("Index", "ShoppingCart");

        }

        #endregion

        // GET: Products/Create
        public ActionResult Create()
        {
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "CategoryName");
            ViewBag.StockStatusID = new SelectList(db.StockStatuses, "StockStatusID", "StockStatus1");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductID,ProductName,CategoryID,UnitPrice,StockStatusID,Description,ProductImage")] Product product, HttpPostedFileBase productImage)
        {
            if (ModelState.IsValid)
            {
                #region File Upload

                string file = "NoImage.png";

                if (productImage != null)
                {
                    file = productImage.FileName;

                    string ext = file.Substring(file.LastIndexOf('.'));

                    string[] goodExts = { ".jpeg", ".jpg", ".png", ".gif" };

                    //Check that the uploaded file ext is in our list of acceptable extensions 
                    //and check that the file size <= 4MB, which is the default maximum for ASP.NET

                    if (goodExts.Contains(ext.ToLower()) && productImage.ContentLength <= 4194304)
                    {
                        //Create a new file name (using a GUID)
                        file = Guid.NewGuid() + ext;

                        #region Resize Image

                        string savePath = Server.MapPath("~/Content/images/");

                        Image convertedImage = Image.FromStream(productImage.InputStream);

                        int maxImageSize = 500;

                        int maxThumbSize = 100;

                        ImageUtility.ResizeImage(savePath, file, convertedImage, maxImageSize, maxThumbSize);

                        #endregion
                    }

                    //No matter what, update the PhotoUrl with the value of the file variable
                    product.ProductImage = file;

                }

                #endregion

                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "CategoryName", product.CategoryID);
            ViewBag.StockStatusID = new SelectList(db.StockStatuses, "StockStatusID", "StockStatus1", product.StockStatusID);
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
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "CategoryName", product.CategoryID);
            ViewBag.StockStatusID = new SelectList(db.StockStatuses, "StockStatusID", "StockStatus1", product.StockStatusID);
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProductID,ProductName,CategoryID,UnitPrice,StockStatusID,Description,ProductImage")] Product product, HttpPostedFileBase productImage)
        {
            if (ModelState.IsValid)
            {
                #region File Upload

                //Check to see if a new file has been uploaded. If not, the HiddenFor()
                //in the view will maintain the original value
                string file = "NoImage.png";

                //If a file has been uploaded
                if (productImage != null)
                {
                    //Get the name
                    file = productImage.FileName;

                    //Capture the extension
                    string ext = file.Substring(file.LastIndexOf('.'));

                    //Create a "whitelist" of accepted exts
                    string[] goodExts = { ".jpeg", ".jpg", ".png", ".gif" };

                    //Check that the uploaded file ext is in our list of acceptable extensions 
                    //and that the file size is <= 4MB

                    if (goodExts.Contains(ext.ToLower()) && productImage.ContentLength <= 4194304)
                    {
                        //Create a new file name (using a GUID)
                        file = Guid.NewGuid() + ext;

                        #region Resize Image

                        string savePath = Server.MapPath("~/Content/imgstore/books/");

                        Image convertedImage = Image.FromStream(productImage.InputStream);

                        int maxImageSize = 500;

                        int maxThumbSize = 100;

                        ImageUtility.ResizeImage(savePath, file, convertedImage, maxImageSize, maxThumbSize);

                        #endregion

                        if (product.ProductImage != null && product.ProductImage != "")
                        {
                            string path = Server.MapPath("~/Content/images/");
                            ImageUtility.Delete(path, product.ProductImage);
                        }

                        //Update the property of the object
                        product.ProductImage = file;
                    }


                }


                #endregion

                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "CategoryName", product.CategoryID);
            ViewBag.StockStatusID = new SelectList(db.StockStatuses, "StockStatusID", "StockStatus1", product.StockStatusID);
            return View(product);
        }

        // GET: Products/Delete/5
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

        // POST: Products/Delete/5
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
