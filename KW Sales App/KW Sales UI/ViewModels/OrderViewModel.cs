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
using System.Collections.Specialized;
using System.Collections.ObjectModel;

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

		private ObservableCollection<CustomerModel> _customers;

		public ObservableCollection<CustomerModel> Customers
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

		private bool _readOnly;

		public bool ReadOnly
		{
			get { return _readOnly; }
			set 
			{ 
				_readOnly = value;
				OnPropertyChanged(nameof(ReadOnly));
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
            Tuple<ObservableCollection<CustomerModel>, string> tuple = CustomerRepository.GetAll().ToTuple();
			if (tuple.Item1 != null)
			{
                Customers = tuple.Item1;
				
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
			if (_state.Equals("Add"))
			{
				if (ValidateCustomer())
				{
					//Save new customer
					Tuple<CustomerModel, string> tuple = _customerRepository!.Insert(Customer).ToTuple();
					if (tuple.Item2 == null)
					{
						//Save successful overwrite CustomerID
						Customers[CustomersIndex] = tuple.Item1;
                        CustomersIndex = Customers.Count - 1;
                    }
					else
					{
                        //Error saving
                        _errorMessage = tuple.Item2;
                        MessageBox.Show("Unable to save the customer!\r\n\r\n" + _errorMessage,
                                        "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
				}
				else
				{
					return;
				}
				SetState("View");
			}
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
					ReadOnly = true;
					break;
				default:
                    CanAddCustomer = false;
					ReadOnly = false;
					break;
			}
		}

		private bool ValidateCustomer()
		{
			bool isValid = true;
			string errorMessage = "";

			//First name validation
			if (string.IsNullOrWhiteSpace(Customer.FirstName))
			{
				errorMessage += "First name is required.\r\n";
				isValid = false;
			}

			//Last name validation
			if (string.IsNullOrWhiteSpace(Customer.LastName))
			{
                errorMessage += "Last name is required.\r\n";
                isValid = false;
            }

            //Last name validation
            if (string.IsNullOrWhiteSpace(Customer.Address))
            {
                errorMessage += "Address is required.\r\n";
                isValid = false;
            }

            //Last name validation
            if (string.IsNullOrWhiteSpace(Customer.City))
            {
                errorMessage += "City is required.\r\n";
                isValid = false;
            }

            //Last name validation
            if (string.IsNullOrWhiteSpace(Customer.State))
            {
                errorMessage += "State is required.\r\n";
                isValid = false;
            }

            //Last name validation
            if (string.IsNullOrWhiteSpace(Customer.Zip))
            {
                errorMessage += "Zip is required.\r\n";
                isValid = false;
            }

            //Show any validation errors
            if (!isValid)
			{
				MessageBox.Show(errorMessage, "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
			}
			return isValid;
		}
    }
}
