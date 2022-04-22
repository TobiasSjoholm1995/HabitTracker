using System;
using System.Globalization;

namespace HabitTracker
{
    static class Constants
    {

        public static readonly CultureInfo Culture = CultureInfo.InvariantCulture;
        public const string ViewDateFormat = "yyyy-MM-dd";
        public const char Separator = '#';
        public const StringComparison Comparer = StringComparison.InvariantCulture;
    }
}
