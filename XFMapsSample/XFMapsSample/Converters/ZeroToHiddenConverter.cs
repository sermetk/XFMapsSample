using System;using System.Globalization;
using Xamarin.Forms;
namespace XFMapsSample.Converters
{
    public class ZeroToHiddenConverter : IValueConverter    {        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)        {            if (value == null)                return false;            if ((string)parameter == "reverse")            {                if (value is int)                    return (int)value == 0;
                if (value is decimal)                    return (decimal)value == 0;                return false;            }            if (value is int)                return (int)value != 0;
            if (value is decimal)                return (decimal)value != 0;            return false;        }        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)        {            throw new NotImplementedException();        }    }
}
