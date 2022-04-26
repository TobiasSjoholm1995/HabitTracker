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


        public static bool IsFirstRun
        {
            get => Preferences.Get(nameof(IsFirstRun), true);
            set => Preferences.Set(nameof(IsFirstRun), value);
        }

        public static bool AutomaticAddToFavoriteHabits
        {
            get => Preferences.Get(nameof(AutomaticAddToFavoriteHabits), false);
            set => Preferences.Set(nameof(AutomaticAddToFavoriteHabits), value);
        }
    }
}
