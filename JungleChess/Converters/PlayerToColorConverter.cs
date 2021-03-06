﻿using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace JungleChess.Converters
{
    public class PlayerToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is PieceColor color)
            {
                if (color == PieceColor.Green)
                {
                    return new SolidColorBrush(Colors.Green);
                }
                else if (color == PieceColor.Red)
                {
                    return new SolidColorBrush(Colors.Red);
                }
            }
            return new SolidColorBrush(Colors.Black);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
