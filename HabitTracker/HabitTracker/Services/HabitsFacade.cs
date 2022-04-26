
using HabitTracker.Models;
using System.Collections.Generic;

namespace HabitTracker.Services
{
    public interface IHabitFacade
    {
        IHabits All();
        IHabits Completed();
    }

    public class HabitsFacade : IHabitFacade
    {
        private Habits _favorites;
        private Habits _completed;

        public HabitsFacade()
        {
            _favorites = new Habits("Favorites" + Database.Extension, true);
            _completed = new Habits("Completed" + Database.Extension);
        }



        public IHabits All()
        {
            return _favorites;
        }

        public IHabits Completed()
        {
            return _completed;
        }
    }
}
