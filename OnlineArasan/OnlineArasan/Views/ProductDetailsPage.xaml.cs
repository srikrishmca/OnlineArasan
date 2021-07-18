
using OnlineArasan.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.CommunityToolkit.Extensions;
namespace OnlineArasan.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProductDetailsPage : ContentPage
    {
        ProductDetailsPageViewModel productDetailsPageViewModel;
        public ProductDetailsPage()
        {
            InitializeComponent();
            productDetailsPageViewModel=new ProductDetailsPageViewModel();
            this.BindingContext = productDetailsPageViewModel;
        }

        private async void ButtonAddToCart_Clicked(object sender, System.EventArgs e)
        {
            if (App.CustomerId == 0)
            {
                await Shell.Current.Navigation.PushModalAsync(new CustomerInfo());
            }
            else
            {
                productDetailsPageViewModel.AddtoCartCommand.Execute(null);
            }

        }
    }
}