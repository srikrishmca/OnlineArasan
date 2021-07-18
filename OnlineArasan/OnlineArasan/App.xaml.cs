using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using OnlineArasan.Models;
namespace OnlineArasan
{
    public partial class App : Application
    {
        public static int CustomerId = 0; // Application level Customer ID 
        public static AddOrderRequest AppOrderRequest;
        public static int OrderId = 0;
        public App()
        {
            InitializeComponent();
            AppOrderRequest = null;
            MainPage = new AppShell();
            //MainPage = new NavigationPage (new Views.HomePage());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
            App.CustomerId = 0;
        }
    }
}
