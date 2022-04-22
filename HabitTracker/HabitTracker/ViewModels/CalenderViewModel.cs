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
        private Habit _selected;
        private DateTime _time;

        public ObservableCollection<Habit> Habits { get; }
        public Command LoadHabitsCommand { get; }
        public Command AddHabitCommand { get; }
        public Command<Habit> HabitClickedCommand { get; }

        public CalenderViewModel()
        {
            _time = DateTime.Today;
            Title = "Calender";
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
                foreach (var habit in CompletedHabits.Get())
                    if(habit.Date.Date == _time.Date)
                        Habits.Add(habit);
            }
            finally
            {
                IsBusy = false;
            }
        }

        public void OnAppearing()
        {
            IsBusy = true;
            SelectedItem = null;
        }

        public Habit SelectedItem
        {
            get => _selected;
            set
            {
                SetProperty(ref _selected, value);
                OnHabitSelected(value);
            }
        }

        private async void OnAddHabit(object obj)
        {
            var habit = new Habit { Name = "Good Social", Score = 1, Date = DateTime.Now };
            await CompletedHabits.Add(habit);
            Habits.Add(habit);
        }

        async void OnHabitSelected(Habit item)
        {
            if (item == null)
                return;

            await Shell.Current.GoToAsync($"{nameof(HabitDetailCalenderPage)}?{nameof(HabitDetailCalenderViewModel.ID)}={item.Id}");
        }
    }
}
