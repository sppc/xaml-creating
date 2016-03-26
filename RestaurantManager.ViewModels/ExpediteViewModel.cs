using System.Collections.Generic;
using System.Collections.ObjectModel;
using RestaurantManager.Models;

namespace RestaurantManager.ViewModels
{
    public class ExpediteViewModel : ViewModel
    {
        protected override void OnDataLoaded()
        {
            OrderItems = new ObservableCollection<Order>(base.Repository.Orders);
        }

        private ObservableCollection<Order> _OrderItems;
        public ObservableCollection<Order> OrderItems
        {
            get { return _OrderItems; }
            private set
            {
                _OrderItems = value;
                NotifyPropertyChanged();
            }
        }
    }
}
