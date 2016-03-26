
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using RestaurantManager.Models;

namespace RestaurantManager.ViewModels
{
    public abstract class ViewModel : INotifyPropertyChanged
    {
        protected RestaurantContext Repository { get; private set; }

        public ViewModel()
        {
            LoadData();
        }

        private async void LoadData()
        {
            IsLoading = true;
            this.Repository = await RestaurantContextFactory.GetRestaurantContextAsync();
            OnDataLoaded();
            IsLoading = false;
        }

        private bool _IsLoading;

        public bool IsLoading
        {
            get { return _IsLoading; }
            set
            {
                _IsLoading = value;
                NotifyPropertyChanged();
            }
        }


        protected abstract void OnDataLoaded();

        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged([CallerMemberName]string propertyName = null)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
