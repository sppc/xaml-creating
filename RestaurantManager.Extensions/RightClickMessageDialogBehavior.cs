using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Microsoft.Xaml.Interactivity;

namespace RestaurantManager.Extensions
{
    public class RightClickMessageDialogBehavior : DependencyObject, IBehavior
    {
        public void Attach(DependencyObject associatedObject)
        {
            if (associatedObject is Page)
            {
                this.AssociatedObject = associatedObject;
                (this.AssociatedObject as Page).RightTapped += Page_RightTapped;
            }
        }

        public void Detach()
        {
            //throw new NotImplementedException();
        }

        public DependencyObject AssociatedObject { get; private set; }

        public string Message { get; set; }

        public string Title { get; set; }

        private void Page_RightTapped(object sender, RightTappedRoutedEventArgs e)
        {
            new MessageDialog(this.Message, this.Title).ShowAsync();
        }
    }
}
