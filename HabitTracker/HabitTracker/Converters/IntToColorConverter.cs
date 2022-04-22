using System;
using Xamarin.Forms;

namespace HabitTracker.Converters
{
    class IntToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var val = value as int?;
            var def  = Color.White;

            if (val == null)
                return def;

            if(val > 0)
                return Color.Green;

            if (val < 0)
                return Color.Red;


            return def;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            // not needed
            return string.Empty;
        }

    }
}
