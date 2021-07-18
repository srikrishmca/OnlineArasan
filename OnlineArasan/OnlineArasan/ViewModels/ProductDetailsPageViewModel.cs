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
    [QueryProperty(nameof(SelectedProductId), nameof(SelectedProductId))]
    public class ProductDetailsPageViewModel : BaseViewModel
    {
        private string _SelectedProductId;
        public string SelectedProductId
        {
            get => _SelectedProductId;
            set
            {
                SetValue(ref _SelectedProductId, value);
                LoadItemId(SelectedProductId);
            }
        }

        private string _PageTitle;
        public string PageTitle
        {
            get => _PageTitle;
            set => SetValue(ref _PageTitle, value);
        }

        private string _ItemsAddedText;
        public string ItemsAddedText
        {
            get => _ItemsAddedText;
            set => SetValue(ref _ItemsAddedText, value);
        }
        public int _ItemsCount;
        public int ItemsCount
        {
            get => _ItemsCount;
            set
            {
                SetValue(ref _ItemsCount, value);
                ItemsAddedText = ItemsCount > 0 ? string.Format("{0} Added", ItemsCount) : "No items added";
            }
        }
        private Product _SelectedProduct;
        public Product SelectedProduct
        {
            get => _SelectedProduct;
            set => SetValue(ref _SelectedProduct, value);
        }


        public ICommand GotToCartCommand { get; }
        public ProductDetailsPageViewModel()
        {
            if (App.AppOrderRequest != null)
            {
                if (App.AppOrderRequest.LstOrderDetails != null)
                {
                    ItemsCount = App.AppOrderRequest.LstOrderDetails.Count;
                }
            }
            else
            {
                ItemsCount = 0;
            }
            GotToCartCommand = new Command(async () =>
            {
                await Application.Current.MainPage.Navigation.PushAsync(new Views.MyCartPage());
            });

            AddtoCartCommand = new Command<Product>(AddToCartFunction);
        }
        public async void LoadItemId(string itemId)
        {
            try
            {
                if (Connectivity.NetworkAccess == NetworkAccess.Internet)
                {
                    string url = string.Format("product/GetProductById?id={0}", itemId);
                    ApiResponse<Product> result = await CommonService.Instance.GetResponseAsync<Product>(url);
                    SelectedProduct = result.Result ?? new Product();
                }
                else
                {
                    SelectedProduct = new Product();
                }
            }
            catch (System.Exception)
            {
                Debug.WriteLine("Failed to Load Item");
            }
        }

        public ICommand AddtoCartCommand { get; }
        private async void AddToCartFunction(Product sender)
        {
            bool itemAlreadyExists = false;
            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                if (App.AppOrderRequest != null)
                {
                    if (App.AppOrderRequest.LstOrderDetails != null)
                    {
                        foreach (OrderDetails item in App.AppOrderRequest.LstOrderDetails)
                        {
                            if (item.ProductId == SelectedProduct.ProductId)
                            {
                                item.Quantity++;
                                itemAlreadyExists = true;
                            }
                        }
                        if (!itemAlreadyExists)
                        {
                            App.AppOrderRequest.LstOrderDetails.Add(new OrderDetails()
                            {
                                Price = SelectedProduct.Price,
                                ProductId = SelectedProduct.ProductId,
                                Quantity = 1
                            });
                        }
                        await DisplayMessage("Items added into the cart");
                    }
                }
                else
                {
                    App.AppOrderRequest = new AddOrderRequest
                    {
                        Order = new Order()
                        {
                            CustomerId = App.CustomerId,
                            OrderTime = System.DateTime.Now,
                            IsActive = true,
                            IsDelivered = false
                        }

                    };
                    App.AppOrderRequest.LstOrderDetails = new List<OrderDetails>
                            {
                                new OrderDetails()
                                {
                                    Price = SelectedProduct.Price,
                                    ProductId = SelectedProduct.ProductId,
                                    Quantity = 1
                                }
                            };

                    await DisplayMessage("Items added into the cart");
                }
            }
            else
            {
                await DisplayMessage("Please check your network connection.");
            }

            ItemsCount = App.AppOrderRequest.LstOrderDetails.Count;

        }
    }
}