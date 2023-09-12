using DataAccessLibrary.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary.Repositories
{
    public class PurchaseRepository : IPurchaseRepository
    {
        private string _connectionString;

        public string ConnectionString { get => _connectionString; private set => _connectionString = value; }

        public PurchaseRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public string Add(PurchaseModel purchase)
        {
            string? errorMessage = null;
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "dbo.usp_AddNewPurchase";
                    command.Parameters.Add("@OrderID", SqlDbType.Int).Value = purchase.OrderID;
                    command.Parameters.Add("@ProductID", SqlDbType.Int).Value = purchase.ProductID;
                    command.Parameters.Add("@Quantity", SqlDbType.Int).Value = purchase.Quantity;
                    command.Parameters.Add("@Price", SqlDbType.Money).Value = purchase.Price;
                    connection.Open();
                    string returnMessage = command.ExecuteScalar().ToString();
                    if (!returnMessage!.Equals("No Error"))
                    {
                        errorMessage = returnMessage;
                    }
                }
            }

            return errorMessage;
        }
    }
}
