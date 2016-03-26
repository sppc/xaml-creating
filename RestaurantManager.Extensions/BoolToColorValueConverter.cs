using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml.Data;

namespace RestaurantManager.Extensions
{
    public class BoolToColorValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            // Convert from bool to Color.
            Color returnValue = Colors.Transparent;
            if (value is bool)
            {
                returnValue = (bool)value ? Colors.Red : Colors.Transparent;
            }
            return returnValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            bool returnValue = default(bool);
            if (value is Color)
            {
                if ((Color)value == Colors.Transparent) { returnValue = false; }
                else if ((Color)value == Colors.Red) { returnValue = true; }
            }
            return returnValue;
        }
    }
}
