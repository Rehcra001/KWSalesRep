using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary.Models
{
    public interface ICustomerRepository
    {
        string ConnectionString { get; set; }
        (CustomerModel, string) Insert(CustomerModel customer);
        (ObservableCollection<CustomerModel>, string) GetAll();
    }
}
