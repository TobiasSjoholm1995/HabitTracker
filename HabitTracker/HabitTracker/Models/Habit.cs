using System;
using System.Globalization;
using System.Text;

namespace HabitTracker.Models
{

    public class Habit
    {
        public string Id { get; set; } 
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public int Score { get; set; }
        public bool Favorite { get; set; }


        public Habit()
        {
            Id = Guid.NewGuid().ToString();
        }

        public override bool Equals(object obj)
        {
            if(obj is Habit h) 
                return string.Equals(h.Id, Id, StringComparison.InvariantCulture);

            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

    }

    public static class HabitConverter
    {
        private const char Separator    = '#';
        private const string DateFormat = "yyyy-MM-dd HH:mm:ss";
        private static readonly CultureInfo Culture = CultureInfo.InvariantCulture;

        public static string Convert(Habit habit)
        {
            if (habit == null)
                return string.Empty;

            return habit.Name + Separator
                 + habit.Id   + Separator 
                 + habit.Date.ToString(DateFormat, Culture) + Separator
                 + habit.Favorite.ToString(Culture) + Separator
                 + habit.Score.ToString(Culture) + Environment.NewLine;
        }

        public static Habit Convert(string line)
        {
            if (string.IsNullOrWhiteSpace(line))
                return null;

            var columns = line.Split(Separator);
            var error = new Exception("The database is invalid.");

            if (columns.Length >= 5)
            {
                return new Habit
                {
                    Name     = GetName(columns, columns.Length - 5) ?? throw error,
                    Id       = columns[columns.Length - 4],
                    Date     = DateTime.ParseExact(columns[columns.Length - 3], DateFormat, Culture),
                    Favorite = bool.Parse(columns[columns.Length - 2]),
                    Score    = int.Parse(columns[columns.Length - 1], NumberStyles.Any, Culture)
                };
            }

            throw error;
        }

        private static string GetName(string[] columns, int index)
        {
            if (columns == null || columns.Length <= index)
                return null;

            var builder = new StringBuilder();

            for (int i = 0; i <= index; i++)
                builder.Append(columns[i] + Separator);

            var name = builder.ToString();
            return name.Length > 1 ? name.Substring(0, name.Length - 1) : null;
        }

    }
}