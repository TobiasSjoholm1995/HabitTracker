using System;
using Xamarin.Forms;

namespace HabitTracker.Converters
{
    class IntToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var val = value as int?;
            var def  = Color.Black;

            if (val == null)
                return def;

            if(val > 0)
                return "#1fa900";

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

    class ScoreToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var val = value as string;
            var def = Color.Black;

            if (val == null)
                return def;

            if (val.StartsWith("+"))
                return "#2eec02";

            if (val.StartsWith("-"))
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
