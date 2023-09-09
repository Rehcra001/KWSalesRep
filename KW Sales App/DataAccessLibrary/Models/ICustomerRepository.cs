using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary.Models
{
    public interface ICustomerRepository
    {
        public string ConnectionString { get; set; }
        CustomerModel Insert(CustomerModel customer);

    }
}
