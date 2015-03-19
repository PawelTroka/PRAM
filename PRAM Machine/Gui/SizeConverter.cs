using System;
using System.Globalization;
using System.Windows.Data;

namespace PRAM_Machine.Gui
{
    internal class SizeConverter : IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int result = int.Parse(value.ToString());
            int offset = int.Parse(parameter.ToString());
            return (result - offset).ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int result = int.Parse(value.ToString());
            int offset = int.Parse(parameter.ToString());
            return (result + offset).ToString();
        }

        #endregion
    }
}