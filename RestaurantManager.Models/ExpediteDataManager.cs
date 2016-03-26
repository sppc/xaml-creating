using System.Collections.Generic;

namespace RestaurantManager.Models
{
    public class ExpediteDataManager : DataManager
    {
        protected override void OnDataLoaded()
        {
            OrderItems = base.Repository.Orders;
        }

        private List<Order> _OrderItems;
        public List<Order> OrderItems
        {
            get { return _OrderItems; }
            private set
            {
                _OrderItems = value;
                OnPropertyChanged();
            }
        }
    }
}
