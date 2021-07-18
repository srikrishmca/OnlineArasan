using OnlineArasan.Models;
using OnlineArasan.Service;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using System.Linq;
using OnlineArasan.Service.Models;

namespace OnlineArasan.ViewModels
{
    public class MyCartViewModel : BaseViewModel
    {
        private bool _IsEmpty;
        public bool IsEmpty
        {
            get => _IsEmpty;
            set => SetValue(ref _IsEmpty, value);
        }
        private string _ErrorMessage = string.Empty;
        public string ErrorMessage
        {
            get => _ErrorMessage;
            set => SetValue(ref _ErrorMessage, value);
        }
        private int _ItemsCount;
        public int ItemsCount
        {
            get => _ItemsCount;
            set => SetValue(ref _ItemsCount, value);
        }
        private Customer _Customer;
        public Customer Customer
        {
            get => _Customer;
            set => SetValue(ref _Customer, value);
        }
        private ObservableCollection<OrderDetailsDisplayInfo> _ProductItemSource;
        public ObservableCollection<OrderDetailsDisplayInfo> ProductItemSource
        {
            get => _ProductItemSource;
            set => SetValue(ref _ProductItemSource, value);
        }


        private bool _IsRefreshing;
        public bool IsRefreshing
        {
            get => _IsRefreshing;
            set => SetValue(ref _IsRefreshing, value);
        }
        public ICommand RefreshCommand { get; }
        public ICommand AddToOrderCommand { get; }
        private async void ConfirmOrderFunction(string sender)
        {
            if (App.AppOrderRequest != null)
            {
                ApiResponse<bool> result = await CommonService.Instance.PostResponseAsync<bool, AddOrderRequest>("Order/AddOrder", App.AppOrderRequest);
                if (result != null)
                {
                    if (result.Result)
                    {
                        App.AppOrderRequest = null;
                        await LoadOrderInfo();
                        await DisplayMessage("Order added successfully");
                    }
                    else
                    {
                        await DisplayMessage("Some went wrong.");
                    }
                }
            }
        }
        public MyCartViewModel()
        {
            AddToOrderCommand = new Command<string>(ConfirmOrderFunction);
        }
        public async Task LoadInitialData()
        {
            if (App.CustomerId > 0)
            {
                await LoadCustomInfo();
            }
            await LoadOrderInfo();
        }
        public async Task LoadCustomInfo()
        {
            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {

                var result = await CommonService.Instance.GetResponseAsync<Customer>("Customer?CustomerId=" + App.CustomerId);
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

        public async Task LoadOrderInfo()
        {
            IsEmpty = false;
            if (App.AppOrderRequest != null)
            {
                if (App.AppOrderRequest.LstOrderDetails != null)
                {
                    string ProductIds = string.Join(",", App.AppOrderRequest.LstOrderDetails.Select(p => p.ProductId.ToString()));
                    ApiResponse<List<Product>> result = await CommonService.Instance.GetResponseAsync<List<Product>>("Product/GetProductByIds?ProductIds=" + ProductIds);
                    if (result.Result != null)
                    {
                        ProductItemSource = new ObservableCollection<OrderDetailsDisplayInfo>();
                        foreach (OrderDetails item in App.AppOrderRequest.LstOrderDetails)
                        {
                            Product SelectedItem = result.Result.Where(i => i.ProductId == item.ProductId).FirstOrDefault();
                            ProductItemSource.Add(new OrderDetailsDisplayInfo()
                            {
                                OrderId = item.OrderId,
                                Price = item.Price,
                                ProductName = SelectedItem != null ? SelectedItem.ProductName : "",
                                Description = SelectedItem != null ? SelectedItem.Description : "",
                                ProductId = item.ProductId,
                                Quantity = item.Quantity,
                                Id = item.Id,
                                ProductImage = SelectedItem != null ? SelectedItem.ImageName : "NoImage.jpg"
                            });
                        }
                    }
                    IsEmpty = false;
                    ErrorMessage = "";
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
    }
}
