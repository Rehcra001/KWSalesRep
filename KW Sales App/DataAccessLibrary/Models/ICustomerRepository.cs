using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary.Models
{
    public interface ICustomerRepository
    {
        string ConnectionString { get; set; }
        (CustomerModel, string) Insert(CustomerModel customer);
        (IEnumerable<CustomerModel>, string) GetAll();
    }
}
