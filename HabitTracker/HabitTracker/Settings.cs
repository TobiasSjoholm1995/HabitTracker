using System;
using System.Globalization;
using Xamarin.Essentials;

namespace HabitTracker
{
    static class Settings
    {

        public static readonly CultureInfo Culture = CultureInfo.InvariantCulture;
        public const string ViewDateOnlyFormat = "yyyy-MM-dd";
        public const string ViewTimeOnlyFormat = "HH:mm";
        public const string ViewDateFormat     = ViewDateOnlyFormat + "  " + ViewTimeOnlyFormat;

        public const char Separator = '#';
        public const StringComparison Comparer = StringComparison.InvariantCulture;
        public const int StartOfWeek = (int)DayOfWeek.Monday;


        public static bool FirstRun
        {
            get => Preferences.Get(nameof(FirstRun), true);
            set => Preferences.Set(nameof(FirstRun), value);
        }
    }
}
