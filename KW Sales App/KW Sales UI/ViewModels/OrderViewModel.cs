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
using KW_Sales_UI.Commands;

namespace KW_Sales_UI.ViewModels
{
    public class OrderViewModel : BaseViewModel
    {
		#region properties
		private string _errorMessage;
		private string _state;

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

		private bool _canAddCustomer;

		public bool CanAddCustomer
		{
			get { return _canAddCustomer; }
			set 
			{ 
				_canAddCustomer = value;
				OnPropertyChanged("CanAddCustomer");
			}
		}


		public RelayCommand AddNewCustomerCommand { get; set; }
		public RelayCommand SaveNewCustomerCommand { get; set; }

        public RelayCommand CancelNewCustomerCommand { get; set; }

        #endregion properties


        public OrderViewModel(INavigationService navigation, 
							  IDataConnectionStringService dataConnection)
        {
			Navigation = navigation;
			DataConnection = dataConnection;
			CustomerRepository = new CustomerRepository(DataConnection.SqlConnectionString);

			AddNewCustomerCommand = new RelayCommand(AddNewCustomer, CanAddNewCustomer);
			SaveNewCustomerCommand = new RelayCommand(SaveNewCustomer, CanSaveNewCustomer);
			CancelNewCustomerCommand = new RelayCommand(CancelNewCustomer, CanCancelNewCustomer);

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
			SetState("View");
        }

        private bool CanCancelNewCustomer(object obj)
        {
			return !_state.Equals("View");
        }

        private void CancelNewCustomer(object obj)
        {
            if (_state.Equals("Add"))
			{
				//Delete the last Record
				int index = Customers.Count - 1;
				Customers.RemoveAt(index);
				SetState("View");
				CustomersIndex = 0;
			}
        }

        private bool CanSaveNewCustomer(object obj)
        {
            return !_state.Equals("View");
        }

        private void SaveNewCustomer(object obj)
        {
            throw new NotImplementedException();
        }

        private bool CanAddNewCustomer(object obj)
        {
			return _state.Equals("View");
        }

        private void AddNewCustomer(object obj)
        {			
			Customers.Add(new CustomerModel());
			CustomersIndex = Customers.Count - 1;
            SetState("Add");
        }

		private void SetState(string state)
		{
			_state = state;

			switch (_state)
			{
				case "View":
                    CanAddCustomer = true;
					break;
				default:
                    CanAddCustomer = false;
					break;
			}
		}
    }
}
