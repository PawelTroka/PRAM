using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;

namespace PRAM_Machine.Gui {
    class SizeConverter : IValueConverter {

        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            //if (value.ToString() == "NaN" || (double)value==double.NaN)
                //return double.NaN;
            
            var result = (int)Math.Round(double.Parse(value.ToString()),0);
            var offset = (int)Math.Round(double.Parse(parameter.ToString()),0);
            return (result - offset).ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            int result = int.Parse(value.ToString());
            int offset = int.Parse(parameter.ToString());
            return (result + offset).ToString();
        }

        #endregion
    }
}
