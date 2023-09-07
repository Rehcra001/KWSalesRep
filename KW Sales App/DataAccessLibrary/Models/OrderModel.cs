using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary.Models
{
    public class OrderModel
    {
        /// <summary>
        /// Holds the orders id
        /// </summary>
        public int OrderID { get; set; }

        /// <summary>
        /// Holds the customers id that this order is linked to
        /// </summary>
        public int CustomerID { get; set; }

        /// <summary>
        /// Holds the date that the order was created
        /// </summary>
        public DateTime OrderDate { get; set; }
    }
}
