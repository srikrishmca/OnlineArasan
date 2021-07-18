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
	public partial class MyOrdersPage : ContentPage
	{
		MyOrdersPageViewModel myOrdersPageViewModel;
		public MyOrdersPage ()
		{
			InitializeComponent ();
			myOrdersPageViewModel = new MyOrdersPageViewModel();
			BindingContext = myOrdersPageViewModel;

		}
		protected override void OnAppearing()
		{
			if (myOrdersPageViewModel != null)
			{
				LoadData();
			}
			base.OnAppearing();
		}
		private void LoadData()
		{
			Task.Run(async () =>
			{
				await myOrdersPageViewModel.LoadInitialData();
			});
		}
	}
}