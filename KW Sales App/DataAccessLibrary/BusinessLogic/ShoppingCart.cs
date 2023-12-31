﻿using DataAccessLibrary.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary.BusinessLogic
{
    public class ShoppingCart : IShoppingCart, INotifyPropertyChanged
    {
        private decimal _total;
        private Dictionary<ProductModel, int> _products;
        private ObservableCollection<string> _productDescriptions;

        public ShoppingCart()
        {
            Products = new Dictionary<ProductModel, int>();
            ProductDescriptions = new ObservableCollection<string>();
            Total = 0;
        }

        public decimal Total { get => _total; set => _total = value;}
        public Dictionary<ProductModel, int> Products { get => _products; set => _products = value; }
        public ObservableCollection<string> ProductDescriptions { get => _productDescriptions; set => _productDescriptions = value; }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void AddToCart(ProductModel product, int quantity)
        {
            //check if product is already in
            if (Products.ContainsKey(product))
            {
                //Exists just update quantity
                Products[product] += quantity;                
            }
            else
            {
                //Not in yet so add
                Products.Add(product, quantity);
            }
            //Caculate Total value
            CalculateTotal();
            //Clear product descriptions and readd
            ProductDescriptions.Clear();
            foreach (KeyValuePair<ProductModel, int> item in Products)
            {
                string description = $"{item.Value} off - {item.Key.Description} - {item.Key.Price:C2}";
                ProductDescriptions.Add(description);
            }
        }

        public void CalculateTotal()
        {
            decimal total = 0;
            if (Products.Count > 0)
            {
                foreach (KeyValuePair<ProductModel, int> item in Products)
                {
                    total += item.Key.Price * item.Value;
                }
                Total = total;
            }
            else
            {
                Total = 0;
            }            
        }

        /// <summary>
        /// Removes a product from the shopping cart
        /// </summary>
        /// <param name="productDescription">
        /// Takes in a product description for parsing
        /// </param>
        public void RemoveFromCart(string productDescription)
        {
            //Parse description to extract the product
            string[] words = productDescription.Split('-');

            foreach (KeyValuePair<ProductModel, int> item in Products)
            {
                if (item.Key.Description!.Equals(words[1].Trim()))
                {
                    Products.Remove(item.Key);
                    break;
                }
            }
            //Recalc total
            CalculateTotal();
            //Clear product descriptions and readd
            ProductDescriptions.Clear();
            foreach (KeyValuePair<ProductModel, int> item in Products)
            {
                string description = $"{item.Value} - {item.Key.Description} - {item.Key.Price}";
                ProductDescriptions.Add(description);
            }
        }
    }
}
