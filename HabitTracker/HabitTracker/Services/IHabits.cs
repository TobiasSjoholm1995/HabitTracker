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

    public interface IHabitFacade
    {
        IHabits All();
        IHabits Completed();
    }
}
