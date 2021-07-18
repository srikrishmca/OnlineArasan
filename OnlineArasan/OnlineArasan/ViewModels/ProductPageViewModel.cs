using OnlineArasan.Models;
using OnlineArasan.Service;
using OnlineArasan.Service.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace OnlineArasan.ViewModels
{
    [QueryProperty(nameof(CategoryId), nameof(CategoryId))]
    [QueryProperty(nameof(PageTitle), nameof(PageTitle))]
    public class ProductPageViewModel : BaseViewModel
    {
        #region Binding Properties 

        private int _CategoryId;
        public int CategoryId
        {
            get => _CategoryId;
            set
            {
                SetValue(ref _CategoryId, value);
                Task.Run(async () =>
                {
                    await LoadProduct(CategoryId);
                });
            }
        }

        private string _PageTitle;
        public string PageTitle
        {
            get => _PageTitle;
            set => SetValue(ref _PageTitle, value);
        }

        private ObservableCollection<Product> _ProductItemSource;
        public ObservableCollection<Product> ProductItemSource
        {
            get => _ProductItemSource;
            set => SetValue(ref _ProductItemSource, value); 
        }

        private bool _IsEmpty;
        public bool IsEmpty
        {
            get => _IsEmpty;
            set => SetValue(ref _IsEmpty, value); 
        }
        bool _IsRefreshing;
        public bool IsRefreshing
        {
            get => _IsRefreshing;
            set => SetValue(ref _IsRefreshing, value); 
        }
        private string _ErrorMessage = string.Empty;
        public string ErrorMessage
        {
            get => _ErrorMessage; 
            set => SetValue(ref _ErrorMessage, value); 
        }
        public ICommand RefreshCommand { get; }

        public ICommand ProductDetailCommand { get; }
        private async void ProductDetailFunction(Product sender)
        {
            await Shell.Current.GoToAsync($"{nameof(Views.ProductDetailsPage)}?{nameof(ProductDetailsPageViewModel.SelectedProductId)}={sender.ProductId}");
            //await App.Current.MainPage.Navigation.PushAsync(new Views.ProductDetailsPage(sender));
        }
        #endregion
        public ProductPageViewModel()
        {
            RefreshCommand = new Command(async () =>
            {
                await LoadProduct(CategoryId);
                // Stop refreshing
                IsRefreshing = false;
            });

            ProductDetailCommand = new Command<Product>(ProductDetailFunction);
        }


        private async Task LoadProduct(int CategoryId)
        {
            //Call the products Service
            IsEmpty = false;
            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                string url = string.Format("product/GetProductbyCategoryId?CategoryId={0}", CategoryId);
                ApiResponse<List<Product>> result = await CommonService.Instance.GetResponseAsync<List<Product>>(url);
                if (result.Result != null)
                {
                    if (result.Result.Count > 0)
                    {
                        IsEmpty = false;
                        ProductItemSource = new ObservableCollection<Product>(result.Result);
                    }
                    else
                    {
                        ErrorMessage = "No data available";
                        IsEmpty = true;
                    }
                }
                else
                {
                    ErrorMessage = "No data available";
                    IsEmpty = true;
                }
            }
            else
            {
                ErrorMessage = "No network connection.";
                IsEmpty = true;
            }
        }
    }
}
