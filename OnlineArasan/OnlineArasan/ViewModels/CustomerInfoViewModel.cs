using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using OnlineArasan.Models;
using OnlineArasan.Service;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace OnlineArasan.ViewModels
{
   public class CustomerInfoViewModel : BaseViewModel
    {
        private Customer _Customer;
        public Customer Customer
        {
            get { return _Customer; }
            set { SetValue(ref _Customer, value); }
        }
        public int Result { get; set; }

        public ICommand EntryUnfocused { get; protected set; }
        public CustomerInfoViewModel()
        {
            Customer = new Customer();
            EntryUnfocused = new Command(CompletedCommandExecutedAsync);
        }
        private async void CompletedCommandExecutedAsync(object param)
        {
            //yourcode...
            if (string.IsNullOrEmpty(Customer.MobileNo))
            {
                await DisplayMessage("Enter customer mobile no.");
                return;
            }
            if (Customer.MobileNo.Length != 10)
            {
                await DisplayMessage("Please enter 10 digit mobile number");
                return;
            }
            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {

                var result = await CommonService.Instance.GetResponseAsync<Customer>("Customer/GetCustomerByNumber?CustomerNumber=" + Customer.MobileNo);
                if (result.Result != null)
                {
                    Customer = result.Result;
                }
            }
            else
            {
                await DisplayMessage("Please check your network connection.");
            }
        }
        
    }

}
