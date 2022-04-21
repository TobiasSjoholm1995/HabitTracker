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
        public const char Separator     = '#';
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

            // >= is for forward compatiblility incase extensions happens in the future
            if (columns.Length >= 5) 
            {
                return new Habit
                {
                    Name     = columns[0],
                    Id       = columns[1],
                    Date     = DateTime.ParseExact(columns[2], DateFormat, Culture),
                    Favorite = bool.Parse(columns[3]),
                    Score    = int.Parse(columns[4], NumberStyles.Any, Culture)
                };
            }

            throw error;
        }

    }
}