using HabitTracker.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HabitTracker.Services
{

    public interface IHabits
    {
        List<Habit> Get();
        Task Add(Habit habit);
        Task Remove(Habit habit);
    }

    public class Habits : IHabits
    {
        private List<Habit> _habits;
        private readonly string _filename;

        public Habits(string filename)
        {
            _filename = filename;
            _habits   = Database.Read(_filename);
        }

        public async Task Add(Habit habit)
        {
            _habits.Add(habit);

            await Database.Save(_filename, _habits);
        }

        public async Task Remove(Habit habit)
        {
            var removed = _habits.Remove(habit);

            if (removed)
                await Database.Save(_filename, _habits);
        }

        public List<Habit> Get()
        {
            return _habits;
        }

    }

}
