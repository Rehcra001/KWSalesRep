using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary.Models
{
    public class ProductModel
    {
        /// <summary>
        /// Holds the unique id for this product
        /// </summary>
        public int ProductID { get; set; }

        /// <summary>
        /// Holds a description of the product
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Holds the current price of the product
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Holds the total number of this product sold
        /// </summary>
        public int NumberSold { get; set; }
    }
}
