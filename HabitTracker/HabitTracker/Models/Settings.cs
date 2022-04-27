using System;
using System.Globalization;
using System.Threading;
using Xamarin.Essentials;

namespace HabitTracker
{
    static class Settings
    {

        public static readonly CultureInfo Culture = CultureInfo.InvariantCulture;
        public const char Separator = '#';
        public const StringComparison Comparer = StringComparison.InvariantCulture;

        public static bool IsFirstRun
        {
            get => Preferences.Get(nameof(IsFirstRun), true);
            set => Preferences.Set(nameof(IsFirstRun), value);
        }

        public static bool IsAutoAddToFavoritesEnabled_Default = false;
        public static bool IsAutoAddToFavoritesEnabled
        {
            get => Preferences.Get(nameof(IsAutoAddToFavoritesEnabled), IsAutoAddToFavoritesEnabled_Default);
            set => Preferences.Set(nameof(IsAutoAddToFavoritesEnabled), value);
        }

        public static int StartOfWeek_Default = (int)DayOfWeek.Monday;

        public static int StartOfWeek
        {
            get => Preferences.Get(nameof(StartOfWeek), StartOfWeek_Default);
            set => Preferences.Set(nameof(StartOfWeek), value);
        }

        public static string ViewDateFormat_Default = "yyyy-MM-dd";

        public static string ViewDateFormat
        {
            get => Preferences.Get(nameof(ViewDateFormat), ViewDateFormat_Default);
            set => Preferences.Set(nameof(ViewDateFormat), value);
        }

    }
}
