using DataAccessLibrary.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private string? _connectionString;
        public string ConnectionString { get => _connectionString!; set => _connectionString = value; }

        public ProductRepository(string connectionString)
        {
            ConnectionString = connectionString;
        }

        public (ObservableCollection<ProductModel>, string) GetAll()
        {
            ObservableCollection<ProductModel> products = new ObservableCollection<ProductModel>();
            string? errorMessage = null;
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "dbo.usp_GetAllProducts";
                    connection.Open();
                    using (SqlDataAdapter adapter = new SqlDataAdapter())
                    {
                        DataTable dataTable = new DataTable();
                        adapter.SelectCommand = command;
                        adapter.Fill(dataTable);

                        if (dataTable.Columns.Count > 1)//data returned
                        {
                            foreach (DataRow row in dataTable.Rows)
                            {
                                ProductModel product = new ProductModel();
                                product.ProductID = Convert.ToInt32(row[dataTable.Columns["ProductID"]!]);
                                product.Description = row[dataTable.Columns["Description"]!].ToString();
                                product.Price = Convert.ToDecimal(row[dataTable.Columns["Price"]!]);
                                product.NumberSold = Convert.ToInt32(row[dataTable.Columns["NumberSold"]!]);
                                products.Add(product);
                            }
                        }
                        else//error returned
                        {
                            errorMessage = dataTable.Columns["Message"]!.ToString();
                        }
                    }
                }
            }

            return (products, errorMessage);
        }
    }
}
