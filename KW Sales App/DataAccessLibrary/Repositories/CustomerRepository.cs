using DataAccessLibrary.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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

        /// <summary>
        /// Inserts a customer into the database and get a CustomerID back
        /// </summary>
        /// <param name="customer">
        /// Takes in a customer model
        /// </param>
        /// <returns>
        /// Return a tuple of customer model and a string.
        /// The string will be set to null if no error message is sent back from the database
        /// otherwise it will contain the error message;
        /// </returns>
        public (CustomerModel, string?) Insert(CustomerModel customer)
        {
            string? returnMessage = null;
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "dbo.usp_InsertCustomer";
                    command.Parameters.AddWithValue("@FirstName", customer.FirstName);
                    command.Parameters.AddWithValue("@LastName", customer.LastName);
                    command.Parameters.AddWithValue("@Address", customer.Address);
                    command.Parameters.AddWithValue("@City", customer.City);
                    command.Parameters.AddWithValue("@State", customer.State);
                    command.Parameters.AddWithValue("@Zip", customer.Zip);
                    connection.Open();
                    returnMessage = command.ExecuteScalar().ToString();
                }
            }
            if (int.TryParse(returnMessage, out _))
            {
                customer.CustomerID = int.Parse(returnMessage);
                returnMessage = null;
            }

            return (customer, returnMessage);
        }

        public (IEnumerable<CustomerModel>, string) GetAll()
        {
            List<CustomerModel> customers = new List<CustomerModel>();
            string? errorMessage = null;
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "dbo.usp_GetAllCustomers";
                    connection.Open();
                    using (SqlDataAdapter adapter = new SqlDataAdapter())
                    {
                        DataTable dataTable = new DataTable();
                        adapter.SelectCommand = command;
                        adapter.Fill(dataTable);
                        if (dataTable.Columns.Count > 1)
                        {
                            foreach (DataRow row in dataTable.Rows)
                            {
                                CustomerModel customer = new CustomerModel();
                                customer.CustomerID = Convert.ToInt32(row[dataTable.Columns["CustomerID"]!]);
                                customer.FirstName = row[dataTable.Columns["FirstName"]!].ToString();
                                customer.LastName = row[dataTable.Columns["LastName"]!].ToString();
                                customer.Address = row[dataTable.Columns["Address"]!].ToString();
                                customer.City = row[dataTable.Columns["City"]!].ToString();
                                customer.State = row[dataTable.Columns["State"]!].ToString();
                                customer.Zip = row[dataTable.Columns["Zip"]!].ToString();

                                customers.Add(customer);
                            }
                        }
                        else
                        {                            
                            errorMessage = dataTable.Columns["Message"]!.ToString();
                        }
                    }
                }
            }
            return (customers, errorMessage);
        }
    }
}
