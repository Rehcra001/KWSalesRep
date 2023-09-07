using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary.Models
{
    public class PurchaseModel
    {
        /// <summary>
        /// Links this purchase to the order model
        /// </summary>
        public int OrderID { get; set; }

        /// <summary>
        /// Links this purchase to the product sold
        /// </summary>
        public int ProductID { get; set; }

        /// <summary>
        /// Holds the amount of this product sold on this order
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// Holds the historic price of the product purchased
        /// </summary>
        public decimal Price { get; set; }
    }
}
