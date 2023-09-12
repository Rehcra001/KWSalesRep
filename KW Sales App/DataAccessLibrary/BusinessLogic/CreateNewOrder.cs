using DataAccessLibrary.Models;
using DataAccessLibrary.Repositories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary.BusinessLogic
{
    public class CreateNewOrder
    {
        private ShoppingCart _shoppingCart;
        private CustomerModel _customer;
        private DateTime _orderDate;
        private OrderModel _order;
        private string _connectionString;

        public CreateNewOrder(string connectionString, ShoppingCart shoppingCart, CustomerModel customer, DateTime orderDate)
        {
            _connectionString = connectionString;
            _shoppingCart = shoppingCart;
            _customer = customer;
            _orderDate = orderDate;
        }

        public int SaveOrder()
        {
            //Create a new order and get the order id back
            _order = new OrderModel() { CustomerID = _customer.CustomerID, OrderDate = _orderDate };
            Tuple<OrderModel, string> order = new OrderRepository(_connectionString).Add(_order).ToTuple(); 
            //Check for errorMessage
            if (order.Item2 == null)
            {
                //No error
                _order = order.Item1;
            }
            else
            {
                throw new Exception(order.Item2);
            }


            //Foreach product in the shopping cart
            foreach (KeyValuePair<ProductModel, int> item in _shoppingCart.Products)
            {
                //Create a new purchase
                PurchaseModel purchase = new PurchaseModel();
                purchase.OrderID = _order.OrderID;
                purchase.ProductID = item.Key.ProductID;
                purchase.Quantity = item.Value;
                purchase.Price = item.Key.Price;

                string errorMessage = new PurchaseRepository(_connectionString).Add(purchase);
                if (errorMessage != null)
                {
                    throw new Exception(errorMessage);
                }
                //and update the total number of product sold

                errorMessage = new ProductRepository(_connectionString).UpdateTotalQuantitySold(item.Key.ProductID, item.Value);
                if (errorMessage != null)
                {
                    throw new Exception(errorMessage);
                }
            }
            //Return the new OrderID;
            return _order.OrderID;
        }
    }
}
