using System;
using System.Globalization;
using System.Windows.Data;

namespace RacingGame.Converters
{
    public class FuelToWidthConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is double fuel)
            {
                double maxWidth = 100;
                return (fuel / 100) * maxWidth;
            }
            return 0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
