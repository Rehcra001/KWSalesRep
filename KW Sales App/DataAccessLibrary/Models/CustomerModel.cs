using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary.Models
{
    public class CustomerModel
    {
        /// <summary>
        /// Holds the customer id
        /// </summary>
        public int CustomerID { get; set; }

        /// <summary>
        /// Holds the customers first name
        /// </summary>
        public string? FirstName { get; set; }

        /// <summary>
        /// Holds the customers last name
        /// </summary>
        public string? LastName { get; set; }

        /// <summary>
        /// Holds the customers address
        /// </summary>
        public string? Address { get; set; }

        /// <summary>
        /// Holds the city the customer resides in
        /// </summary>
        public string? City { get; set; }

        /// <summary>
        /// Holds the state the customer resides in
        /// </summary>
        public string? State { get; set; }

        /// <summary>
        /// Holds the zip code where the customer resides
        /// </summary>
        public string? Zip { get; set; }

        /// <summary>
        /// Concatenation of the customers last name, first name, and their customer id
        /// </summary>
        public string? FullName => $"{LastName}, {FirstName} ({CustomerID})";
    }
}
