using DataAccessLibrary.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace DataAccessLibrary.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private string _connectionString;
        public string ConnectionString { get => _connectionString; private set => _connectionString = value; }

        public OrderRepository(string connectionString)
        {
            ConnectionString = connectionString;
        }



        public (OrderModel, string) Add(OrderModel order)
        {
            string? errorMessage = null;
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "dbo.usp_AddNewOrder";
                    command.Parameters.Add("@CustomerID", SqlDbType.Int).Value = order.CustomerID;
                    command.Parameters.Add("@OrderDate", SqlDbType.DateTime).Value = order.OrderDate;
                    connection.Open();

                    //Returns either an order id or an error message
                    string returnMessage = command.ExecuteScalar().ToString()!;
                    if (int.TryParse(returnMessage, out _))
                    {
                        //ID returned
                        order.OrderID = int.Parse(returnMessage);
                    }
                    else
                    {
                        //error returned
                        errorMessage = returnMessage;
                    }
                }
            }

            return (order, errorMessage);
        }
    }
}
