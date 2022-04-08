using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StoreFront3.DATA.EF; //Added for access to the Entity Models (Books)
using System.ComponentModel.DataAnnotations; //Added for access to validation/display metadata attributes

namespace StoreFront3.UI.MVC.Models
{
    //Added this model to combine Domain/Entity-related info (Book) with
    //other info -- Therefor, it's a ViewModel

    public class CartItemViewModel
    {
        [Key]
        [Range(1, int.MaxValue)]
        public int Qty { get; set; }

        [Key]
        public Product Product { get; set; }


        public CartItemViewModel(int qty, Product product)
        {
            Qty = qty;
            Product = product;
        }
    }
}