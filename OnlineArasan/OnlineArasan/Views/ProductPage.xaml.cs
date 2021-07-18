using OnlineArasan.ViewModels;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
namespace OnlineArasan.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProductPage : ContentPage
    {
        ProductPageViewModel productPageViewModel;
        public ProductPage()
        {
            InitializeComponent();
            productPageViewModel = new ProductPageViewModel();
            BindingContext = productPageViewModel;
        }
        private bool isClicked = true;
        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            if (isClicked)
            {
                isClicked = false;
                object result = (sender as Grid).BindingContext;
                await Task.Delay(1000);
                isClicked = true;
                productPageViewModel.ProductDetailCommand.Execute(result);
            }
        }
    }
}