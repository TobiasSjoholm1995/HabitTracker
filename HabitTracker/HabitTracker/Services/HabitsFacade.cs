
namespace HabitTracker.Services
{
    public class HabitsFacade : IHabitFacade
    {
        private Habits _all;
        private Habits _completed;

        public HabitsFacade()
        {
            _all = new Habits("All" + Database.Extension);
            _completed = new Habits("Completed" + Database.Extension);
        }

        public IHabits All()
        {
            return _all;
        }

        public IHabits Completed()
        {
            return _completed;
        }
    }
}
