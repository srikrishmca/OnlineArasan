using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;
using OnlineArasan.ViewModels.Popups;
using OnlineArasan.Service;
namespace OnlineArasan.Views.Popups
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ContactInfo : Popup
    {
        ContactInfoViewModel _ContactInfoViewModel;
        public ContactInfo()
        {
            InitializeComponent();
            _ContactInfoViewModel = new ContactInfoViewModel();
            this.BindingContext = _ContactInfoViewModel;
        }

        private void BtnCancel_Clicked(object sender, EventArgs e)
        {
            Dismiss(null);
        }

        private async void BtnSave_Clicked(object sender, EventArgs e)
        {
            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                ///api/Customer/InsertUpdateCustomer
                ///
                var result = await CommonService.Instance.PostResponseAsync<int, Models.Customer>("Customer/InsertUpdateCustomer", _ContactInfoViewModel.Customer);
                if (result != null)
                {
                    if (result.Result > 0)
                    {
                        // success fully saved 
                        await App.Current.MainPage.DisplayAlert("Lingesh Furniture", "Contact Saved.", "Ok");
                        Dismiss(null);
                    }
                    else
                    {
                        await App.Current.MainPage.DisplayAlert("Lingesh Furniture", result.Message, "Ok");
                    }
                }
            }
        }
    }
}