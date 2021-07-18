﻿using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace OnlineArasan.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        protected void SetValue<T>(ref T backingField, T value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingField, value))
            {
                return;
            }

            backingField = value;
            OnPropertyChanged(propertyName);

        }

        protected async Task DisplayMessage(string Message)
        {
            await Xamarin.Forms.Application.Current.MainPage.DisplayAlert("Lingesh Furniture", Message, "Ok");
        }
    }
}
