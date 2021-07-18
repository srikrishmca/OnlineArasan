using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using OnlineArasan.ViewModels;
namespace OnlineArasan.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : ContentPage
    {
        HomePageViewModel _homePageViewModel;
        private bool isClicked = true;
        public HomePage()
        {
            InitializeComponent();
            _homePageViewModel = new HomePageViewModel();
            BindingContext = _homePageViewModel;

        }

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            if (isClicked)
            {
                isClicked = false;
                object result = (sender as Grid).BindingContext;
                await Task.Run(async () =>
                {
                    await Task.Delay(1000);
                    isClicked = true;
                });
                _homePageViewModel.OnItemCommand.Execute(result);
            }
        }
    }
}