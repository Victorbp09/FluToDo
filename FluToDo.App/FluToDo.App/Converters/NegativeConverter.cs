using System;
using System.Globalization;
using Xamarin.Forms;

namespace FluToDo.App.Converters
{
    // Invert the parameter value. i.e. true => false , false => true
    public class NegativeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool boolvalue = false;
            if (value != null && bool.TryParse(value.ToString(), out boolvalue))
            {
                return (!boolvalue);
            }
            return boolvalue;

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
