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
    public class MyOrdersPageViewModel :BaseViewModel
    {
        private OrderResponse _ProductItemSource;
        public OrderResponse ProductItemSource
        {
            get => _ProductItemSource;
            set => SetValue(ref _ProductItemSource, value);
        }

        private ObservableCollection<OrderDetailsDisplayInfo> _OrderItemSource;
        public ObservableCollection<OrderDetailsDisplayInfo> OrderItemSource
        {
            get => _OrderItemSource;
            set => SetValue(ref _OrderItemSource, value);
        }
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

        public async Task LoadInitialData()
        {
            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                if (App.CustomerId > 0)
                {
                    ApiResponse<OrderResponse> result1 = await CommonService.Instance.GetResponseAsync<OrderResponse>("Order/GetOrderByCustomerId?CustomerId=" + App.CustomerId);
                    if (result1.Result != null)
                    {
                        ProductItemSource = result1.Result;
                        string ProductIds = string.Join(",", ProductItemSource.LstOrderDetails.Select(p => p.ProductId.ToString()));
                        ApiResponse<List<Product>> result = await CommonService.Instance.GetResponseAsync<List<Product>>("Product/GetProductByIds?ProductIds=" + ProductIds);
                        if (result.Result != null)
                        {

                            OrderItemSource = new ObservableCollection<OrderDetailsDisplayInfo>();

                            foreach (Order OrderItem in ProductItemSource.LstOrder)
                            {
                                var LstOrderDetails = ProductItemSource.LstOrderDetails.Where(i => i.OrderId == OrderItem.OrderId);
                                foreach (OrderDetails item in LstOrderDetails)
                                {
                                    Product SelectedItem = result.Result.Where(i => i.ProductId == item.ProductId).FirstOrDefault();
                                    OrderItemSource.Add(new OrderDetailsDisplayInfo()
                                    {
                                        OrderId = item.OrderId,
                                        Price = item.Price,
                                        ProductName = SelectedItem != null ? SelectedItem.ProductName : "",
                                        Description = SelectedItem != null ? SelectedItem.Description : "",
                                        ProductId = item.ProductId,
                                        Quantity = item.Quantity,
                                        Id = item.Id,
                                        OrderDateTime = OrderItem.OrderTime,
                                        ProductImage = SelectedItem != null ? SelectedItem.ImageName : "NoImage.jpg",
                                        Delivered = OrderItem.IsDelivered
                                    });
                                }
                            }
                            IsEmpty = false;
                        }
                    }
                    else
                    {
                        IsEmpty = true;
                        ErrorMessage = "No items in your list.";
                    }
                }
                else
                {

                    IsEmpty = true;
                    ErrorMessage = "No items in your list.";
                }
            }
            else
            {
                IsEmpty = true;
                ErrorMessage = "No items in your list.";
                await DisplayMessage("Please check your network connection.");
            }

        }

    }
}
