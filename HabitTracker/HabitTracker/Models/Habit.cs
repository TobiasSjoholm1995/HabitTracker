using System;
using static HabitTracker.Settings;

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
                return string.Equals(h.Id, Id, Comparer);

            if(obj is string s)
                return string.Equals(s, Id, Comparer);

            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

    }
}