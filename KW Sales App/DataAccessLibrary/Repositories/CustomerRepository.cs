using DataAccessLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private string? _connectionString;
        public string ConnectionString { get => _connectionString!; set => _connectionString = value; }

        public CustomerRepository(string connectionString)
        {
            ConnectionString = connectionString;
        }

        public CustomerModel Insert(CustomerModel customer)
        {
            throw new NotImplementedException();
        }
    }
}
