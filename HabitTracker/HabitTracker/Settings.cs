using System;
using System.Globalization;
using Xamarin.Essentials;

namespace HabitTracker
{
    static class Settings
    {

        public static readonly CultureInfo Culture = CultureInfo.InvariantCulture;
        public const string ViewDateFormat = "yyyy-MM-dd";
        public const char Separator = '#';
        public const StringComparison Comparer = StringComparison.InvariantCulture;
        public const int StartOfWeek = (int)DayOfWeek.Monday;
        public const bool AutoAddHabitToFavorites = false;


        public static bool IsFirstRun
        {
            get => Preferences.Get(nameof(IsFirstRun), true);
            set => Preferences.Set(nameof(IsFirstRun), value);
        }

    }
}
