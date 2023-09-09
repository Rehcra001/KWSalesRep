using KW_Sales_UI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLibrary.Repositories;
using DataAccessLibrary.Models;
using System.CodeDom.Compiler;
using System.Windows;

namespace KW_Sales_UI.ViewModels
{
    public class OrderViewModel : BaseViewModel
    {
		#region properties
		private string _errorMessage;

        private ICustomerRepository? _customerRepository;

		public ICustomerRepository? CustomerRepository
        {
			get { return _customerRepository; }
			private set { _customerRepository = value; }
		}


		//Navigation between views
		private INavigationService? _navigationService;

		public INavigationService? Navigation
		{
			get { return _navigationService; }
			set 
			{ 
				_navigationService = value;
				OnPropertyChanged("Navigation");
			}
		}

		//datasource connection string
		private IDataConnectionStringService? _connection;

		public IDataConnectionStringService? DataConnection
		{
			get { return _connection; }
			set 
			{ 
				_connection = value;
				OnPropertyChanged("DataConnection");
			}
		}

		private CustomerModel _customer;

		public CustomerModel Customer
		{
			get { return _customer; }
			set 
			{ 
				_customer = value;
				OnPropertyChanged("Customer");
			}
		}

		private List<CustomerModel> _customers;

		public List<CustomerModel> Customers
		{
			get { return _customers; }
			set 
			{ 
				_customers = value;
				OnPropertyChanged("Customers");
			}
		}

		private int _customersIndex;

		public int CustomersIndex
		{
			get { return _customersIndex; }
			set 
			{ 
				_customersIndex = value;
				OnPropertyChanged("CustomersIndex");
			}
		}

		#endregion properties


		public OrderViewModel(INavigationService navigation, 
							  IDataConnectionStringService dataConnection)
        {
			Navigation = navigation;
			DataConnection = dataConnection;
			CustomerRepository = new CustomerRepository(DataConnection.SqlConnectionString);

			//Load all Customers
            Tuple<IEnumerable<CustomerModel>, string> tuple = CustomerRepository.GetAll().ToTuple();
			if (tuple.Item1 != null)
			{
                Customers = tuple.Item1.ToList();
            }
			else
			{
				_errorMessage = tuple.Item2;
				MessageBox.Show("Unable to retrieve the list of customers!\r\n\r\n" + _errorMessage,
								"ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
				return;
			}
        }
    }
}
