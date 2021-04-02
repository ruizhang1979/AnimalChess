using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace JungleChess.Converters
{
    public class CurrentPlayerToBorderColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Player p && parameter is string s)
            {
                if ((p.MoveFirst && s == "A") ||
                    (!p.MoveFirst && s == "B"))
                {
                    return new SolidColorBrush(Colors.Yellow);
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