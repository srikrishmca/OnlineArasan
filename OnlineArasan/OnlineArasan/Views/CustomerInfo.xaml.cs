using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using OnlineArasan.ViewModels;
using Xamarin.Essentials;
using OnlineArasan.Service;

namespace OnlineArasan.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CustomerInfo : ContentPage
    {
        CustomerInfoViewModel _CustomerInfoViewModel;
        public CustomerInfo()
        {
            InitializeComponent();
            _CustomerInfoViewModel = new CustomerInfoViewModel();
            this.BindingContext = _CustomerInfoViewModel;
        }
        private async void BtnCancel_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.Navigation.PopModalAsync();
        }

        private async void BtnSave_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_CustomerInfoViewModel.Customer.MobileNo))
            {
                await DisplayMessage("Enter customer mobile no.");
                return;
            }
            if (_CustomerInfoViewModel.Customer.MobileNo.Length != 10)
            {
                await DisplayMessage("Please enter 10 digit mobile number");
                return;
            }
            if (string.IsNullOrEmpty(_CustomerInfoViewModel.Customer.CustomerName))
            {
                await DisplayMessage("Enter Customer Name.");
                return;
            }
            if (string.IsNullOrEmpty(_CustomerInfoViewModel.Customer.MobileNo))
            {
                await DisplayMessage("Enter customer mobile no.");
                return;
            }

            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {

                var result = await CommonService.Instance.PostResponseAsync<int, Models.Customer>("Customer/InsertUpdateCustomer", _CustomerInfoViewModel.Customer);
                if (result != null)
                {
                    if (result.Result > 0)
                    {
                        // success fully saved 

                        App.CustomerId = result.Result;
                        await App.Current.MainPage.DisplayAlert("Lingesh Furniture", "Contact Saved.", "Ok");
                        await Shell.Current.Navigation.PopModalAsync();
                    }
                    else
                    {
                        await App.Current.MainPage.DisplayAlert("Lingesh Furniture", result.Message, "Ok");
                    }
                }
                else
                {
                    await DisplayMessage("Some thing went wrong.");
                }
            }
        }

        private async Task DisplayMessage(string Message)
        {
            await App.Current.MainPage.DisplayAlert("Lingesh Furniture", Message, "Ok");
        }
    }
}