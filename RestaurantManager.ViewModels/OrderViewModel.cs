using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation.Diagnostics;
using RestaurantManager.Models;
using RestaurantManager.ViewModels;

namespace RestaurantManager.ViewModels
{
    public class OrderViewModel : ViewModel
    {
        public DelegateCommand<string> AddToSelectionCommand { get; private set; }
        public DelegateCommand<string> SubmitOrderCommand { get; private set; }

        public OrderViewModel()
        {
            AddToSelectionCommand = new DelegateCommand<string>(AddSelectionToOrder, CanExecuteSelectionToOrder);
            SubmitOrderCommand = new DelegateCommand<string>(SubmitOrder, CanExecuteSubmitOrder);
        }

        private bool CanExecuteSubmitOrder(string arg)
        {
            return CurrentlySelectedMenuItems != null && CurrentlySelectedMenuItems.Count > 0;
        }

        private void SubmitOrder(string obj)
        {
            Repository.Orders.Add(new Order
            {
                Complete = false,
                Expedite = false,
                Id = 1,
                Items = new List<MenuItem>(CurrentlySelectedMenuItems),
                SpecialRequests = "",
                Table = Repository.Tables[0]
            });

            foreach (var currentlySelectedMenuItem in CurrentlySelectedMenuItems)
            {
                MenuItems.Add(currentlySelectedMenuItem);
            }
            
            CurrentlySelectedMenuItems.Clear();
            CurrentMenuItem = null;
        }

        private void AddSelectionToOrder(string message)
        {
            if (CurrentMenuItem != null)
            {
                CurrentlySelectedMenuItems.Add(CurrentMenuItem);
            }

            MenuItems.Remove(CurrentMenuItem);
            CurrentMenuItem = null;            
        }

        private bool CanExecuteSelectionToOrder(string message)
        {
            return CurrentMenuItem != null;
        }

        protected override void OnDataLoaded()
        {
            this.MenuItems = new ObservableCollection<MenuItem>(base.Repository.StandardMenuItems);
            this.CurrentlySelectedMenuItems = new ObservableCollection<MenuItem>();
        }

        private ObservableCollection<MenuItem> _MenuItems;
        public ObservableCollection<MenuItem> MenuItems
        {
            get { return _MenuItems; }
            set
            {
                _MenuItems = value;
                NotifyPropertyChanged();
            }
        }

        private ObservableCollection<MenuItem> _CurrentlySelectedMenuItems;
        public ObservableCollection<MenuItem> CurrentlySelectedMenuItems
        {
            get { return _CurrentlySelectedMenuItems; }
            set
            {
                _CurrentlySelectedMenuItems = value;
                NotifyPropertyChanged();
            }
        }

        private MenuItem _CurrentMenuItem;

        public MenuItem CurrentMenuItem
        {
            get
            {
                return _CurrentMenuItem;
            }

            set
            {
                _CurrentMenuItem = value;
                NotifyPropertyChanged();
                AddToSelectionCommand.RaiseCanExecuteChanged();
                SubmitOrderCommand.RaiseCanExecuteChanged();
            }
        }
    }
}
