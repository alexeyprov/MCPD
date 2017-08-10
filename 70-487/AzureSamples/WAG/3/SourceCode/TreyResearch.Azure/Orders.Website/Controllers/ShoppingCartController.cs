//===============================================================================
// Microsoft patterns & practices
// Windows Azure Architecture Guide
//===============================================================================
// Copyright © Microsoft Corporation.  All rights reserved.
// This code released under the terms of the 
// Microsoft patterns & practices license (http://wag.codeplex.com/license)
//===============================================================================


namespace Orders.Website.Controllers
{
    using System.Web.Mvc;
    using Orders.Shared;
    using Orders.Website.CustomAttributes;
    using Orders.Website.DataStores;
    using Orders.Website.Models;
    using Orders.Website.ViewModels;

    [LogAction]
    public class ShoppingCartController : Controller
    {
        private readonly IProductStore productStore;
        private readonly ICartStore cartStore;

        public ShoppingCartController(IProductStore productStore, ICartStore cartStore)
        {
            Guard.CheckArgumentNull(productStore, "productStore");
            Guard.CheckArgumentNull(cartStore, "cartStore");

            this.productStore = productStore;
            this.cartStore = cartStore;
        }

        public ActionResult Index()
        {   
            var cartId = ShoppingCart.GetCartId(this.HttpContext);

            var vm = new ShoppingCartViewModel
            { 
                CartItems = this.cartStore.FindCartItems(cartId),
                CartTotal = this.cartStore.GetTotal(cartId)
            };

            return View(vm);
        }

        public ActionResult AddToCart(int id)
        {
            var addedProduct = this.productStore.FindOne(id);

            // Add it to the shopping cart
            var cartId = ShoppingCart.GetCartId(this.HttpContext);

            this.cartStore.AddItem(cartId, addedProduct);

            // Go back to the main store page for more shopping
            return RedirectToAction("Index");
        }

        public JsonResult RemoveFromCart(int id)
        {
            //// Get the name of the album to display confirmation
            var cartId = ShoppingCart.GetCartId(this.HttpContext);
            var product = this.cartStore.FindItem(id);
            string productName = product.Product.Description;

            //// Remove from cart
            int itemCount = this.cartStore.Delete(id);

            // Display the confirmation message
            var results = new ShoppingCartRemoveViewModel
            {
                Message = Server.HtmlEncode(productName) +
                    " has been removed from your shopping cart.",
                CartTotal = this.cartStore.GetTotal(cartId),
                CartCount = this.cartStore.GetCount(cartId),
                ItemCount = itemCount,
                DeleteId = id
            };

            return Json(results);
        }

        [ChildActionOnly]
        public ActionResult CartSummary()
        {
            var cartId = ShoppingCart.GetCartId(this.HttpContext);
            ViewData["CartCount"] = this.cartStore.GetCount(cartId);

            return PartialView("CartSummary");
        }
    }
}
