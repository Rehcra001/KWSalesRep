using DataAccessLibrary.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary.BusinessLogic
{
    public interface IShoppingCart
    {
        /// <summary>
        /// Holds the total value of the shopping cart
        /// </summary>
        public decimal Total { get; set; }

        /// <summary>
        /// Holds a list of products in the shopping cart and the quantity required
        /// </summary>
        public Dictionary<ProductModel, int> Products { get; set; }

        public ObservableCollection<string> ProductDescriptions { get; set; }

        void AddToCart(ProductModel product, int quantity);

        void RemoveFromCart(ProductModel product);

        void CalculateTotal();
    }
}
