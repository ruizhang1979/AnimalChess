﻿using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace JungleChess.Converters
{
    public class BoolToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool b)
            {
                if (b)
                {
                    return new SolidColorBrush(Colors.DarkBlue);
                }
            }
            return new SolidColorBrush(Colors.Transparent);
        }


        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
