using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using OnlineArasan.Models;
using Xamarin.Forms;
using OnlineArasan.Service;
using Xamarin.Essentials;
using OnlineArasan.Views;
using OnlineArasan.Service.Models;

namespace OnlineArasan.ViewModels
{
    public class HomePageViewModel : BaseViewModel
    {
        #region Binding Properties 
        private ObservableCollection<Category> _categoryItemSource;
        public ObservableCollection<Category> CategoryItemSource
        {
            get => _categoryItemSource;
            set => SetValue(ref _categoryItemSource, value);
        }
        private int _CollectionViewHeight;
        public int CollectionViewHeight
        {
            get => _CollectionViewHeight;
            set => SetValue(ref _CollectionViewHeight, value);
        }

        private bool _IsEmpty;
        public bool IsEmpty
        {
            get => _IsEmpty;
            set => SetValue(ref _IsEmpty, value);
        }
        private bool _IsRefreshing;
        public bool IsRefreshing
        {
            get => _IsRefreshing;
            set => SetValue(ref _IsRefreshing, value);
        }
        #endregion

        #region Command function

        public ICommand OnItemCommand { get; set; }
        private async void DoOperation(Category sender)
        {
            await Shell.Current.GoToAsync($"{nameof(ProductPage)}?{nameof(ProductPageViewModel.CategoryId)}={sender.CategoryId}&PageTitle={sender.CategoryName}");
        }

        public ICommand RefreshCommand { get; }
        #endregion

        public HomePageViewModel()
        {

            OnItemCommand = new Command<Category>(DoOperation);

            RefreshCommand = new Command(() =>
            {
                Task.Run(async () =>
                {
                    await LoadTask();
                });
                // Stop refreshing
                IsRefreshing = false;
            });

            Task.Run(async () =>
            {
                await LoadTask();
            });
        }
        private async Task LoadTask()
        {
            CollectionViewHeight = 130;
            IsEmpty = true;
            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                ApiResponse<List<Category>> result = await CommonService.Instance.GetResponseAsync<List<Category>>("Category");
                if (result.Result != null)
                {
                    IsEmpty = false;
                    if (result.Result.Count > 4)
                    {
                        CollectionViewHeight = 260;
                    }
                    CategoryItemSource = new ObservableCollection<Category>(result.Result);
                }
            }
        }
    }
}