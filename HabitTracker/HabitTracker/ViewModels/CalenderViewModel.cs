using HabitTracker.Models;
using HabitTracker.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;

namespace HabitTracker.ViewModels
{
    class CalenderViewModel : BaseViewModel
    {
        public ObservableCollection<Habit> Habits { get; }
        public Command LoadHabitsCommand { get; }
        public Command AddHabitCommand { get; }
        public Command<Habit> HabitClickedCommand { get; }

        private DateTime _time;

        public CalenderViewModel()
        {
            _time  = DateTime.Today;
            Title  = _time.ToString("dddd");
            Habits = new ObservableCollection<Habit>();

            LoadHabitsCommand   = new Command(LoadHabits);
            HabitClickedCommand = new Command<Habit>(OnHabitSelected);
            AddHabitCommand     = new Command(OnAddHabit);
        }

        void LoadHabits()
        {
            IsBusy = true;

            try
            {
                Habits.Clear();
                foreach (var habit in AllHabits.Get())
                {
                    if(habit.Date == _time)
                        Habits.Add(habit);
                }
            }
            finally
            {
                IsBusy = false;
            }
        }

        public void OnAppearing()
        {
            IsBusy = true;
        }

        private async void OnAddHabit(object obj)
        {
            await Shell.Current.GoToAsync(nameof(NewHabitPage));
        }

        async void OnHabitSelected(Habit item)
        {
            if (item == null)
                return;

            // This will push the ItemDetailPage onto the navigation stack
            await Shell.Current.GoToAsync($"{nameof(HabitDetailPage)}?{nameof(HabitDetailViewModel.ID)}={item.Id}");
        }
    }
}
