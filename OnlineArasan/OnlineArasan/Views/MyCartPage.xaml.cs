using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using OnlineArasan.ViewModels;
using OnlineArasan.Models;
using System.Threading.Tasks;

namespace OnlineArasan.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MyCartPage : ContentPage
    {
        readonly MyCartViewModel myCartViewModel;
        public MyCartPage()
        {
            InitializeComponent();
            myCartViewModel = new MyCartViewModel();
            BindingContext = myCartViewModel;

        }

        protected override void OnAppearing()
        {
            if (myCartViewModel != null)
            {
                myCartViewModel.Customer = new Customer();
                LoadData();
            }
            base.OnAppearing();
        }
        private void LoadData()
        {
            Task.Run(async () =>
            {
                await myCartViewModel.LoadInitialData();
            });
        }
        private async void BtnCancel_Clicked(object sender, EventArgs e)
        {
            bool prompt = await DisplayAlert("Lingesh Furniture", "Are you sure want to cancel the order", "OK", "Cancel");
            if (prompt)
            {
                App.AppOrderRequest = null;
                LoadData();
                await DisplayAlert("Lingesh Furniture", "Order Cancelled.", "Ok");
            }
        }
        private void BtnSave_Clicked(object sender, EventArgs e)
        {
            myCartViewModel.AddToOrderCommand.Execute(null);
        }
    }
}