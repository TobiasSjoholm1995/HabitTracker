using HabitTracker.Models;
using HabitTracker.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using Xamarin.Forms;
using static HabitTracker.Constants;

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
        public Command LeftSwipeCommand { get; }
        public Command RightSwipeCommand { get; }



        public CalenderViewModel()
        {
            _time = DateTime.Today;
            Habits = new ObservableCollection<Habit>();

            LoadHabitsCommand   = new Command(LoadHabits);
            HabitClickedCommand = new Command<Habit>(OnHabitSelected);
            AddHabitCommand     = new Command(OnAddHabit);
            LeftSwipeCommand    = new Command(OnLeftSwipe);
            RightSwipeCommand   = new Command(OnRightSwipe);
        }

        private void OnLeftSwipe(object obj)
        {
            _time = _time.AddDays(-1);
            LoadHabits();
        }

        private void OnRightSwipe(object obj)
        {
            _time = _time.AddDays(1);
            LoadHabits();
        }

        private IEnumerable<Habit> GetHabits()
        {
            foreach (var habit in CompletedHabits.Get())
                if (habit.Date.Date == _time.Date)
                    yield return habit;
        }

        private void SetTitle()
        {
            Title = _time.ToString("dddd") + "  " + GetScore();
        }

        private string GetScore()
        {
            var habits = GetHabits()?.ToList();

            if(habits == null || habits.Count == 0)
                return string.Empty;

            var scoreInteger = habits.Sum(h => h.Score);
            var scoreString  = scoreInteger.ToString(CultureInfo.InvariantCulture);

            if (scoreInteger > 0)
                return "+" + scoreString;

            return scoreString;
        }

        void LoadHabits()
        {
            IsBusy = true;

            try
            {
                Habits.Clear();
                foreach (var habit in GetHabits())
                    Habits.Add(habit);

                SetTitle();
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
            await Shell.Current.GoToAsync($"{nameof(SelectHabitPage)}?{nameof(SelectHabitViewModel.Time)}={_time.ToString(ViewDateFormat, Culture)}");

        }

        async void OnHabitSelected(Habit item)
        {
            if (item == null)
                return;

            await Shell.Current.GoToAsync($"{nameof(HabitDetailCalenderPage)}?{nameof(HabitDetailCalenderViewModel.ID)}={item.Id}");
        }
    }
}
