using KW_Sales_UI.Commands;
using KW_Sales_UI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KW_Sales_UI.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
		private INavigationService _navigation;

		public INavigationService Navigation
		{
			get { return _navigation; }
			set 
			{ 
				_navigation = value;
				OnPropertyChanged("Navigation");
			}
		}

        public RelayCommand NavigateToOrderViewCommand { get; set; }

        public MainViewModel(INavigationService navigation)
        {
			Navigation = navigation;

			NavigateToOrderViewCommand = new RelayCommand(NavigateToOrderView, CanNavigateToOrderView);
        }

        private bool CanNavigateToOrderView(object obj)
        {
            return true;
        }

        private void NavigateToOrderView(object obj)
        {
            Navigation.NavigateTo<OrderViewModel>();
        }
    }
}
