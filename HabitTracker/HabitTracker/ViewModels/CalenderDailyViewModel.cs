using HabitTracker.Models;
using HabitTracker.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using Xamarin.Forms;
using static HabitTracker.Settings;

namespace HabitTracker.ViewModels
{
    [QueryProperty(nameof(Time), nameof(Time))]

    class CalenderDailyViewModel : BaseViewModel
    {
        private Habit _selected;
        private DateTime _time;
        private string _score;
        private string _subTitle;

        public ObservableCollection<Habit> Habits { get; }
        public Command LoadHabitsCommand { get; }
        public Command AddHabitCommand { get; }
        public Command<Habit> HabitClickedCommand { get; }
        public Command PreviousDayCommand { get; }
        public Command NextDayCommand { get; }
        public Command TodayCommand { get; }

        public string Time
        {
            get
            {
                return _time.ToString(ViewDateFormat);
            }
            set
            {
                if(DateTime.TryParseExact(value, ViewDateFormat, Culture, DateTimeStyles.AssumeUniversal, out var date))
                {
                    _time = date;
                    LoadHabits();
                }
            }
        }


        public string Score
        {
            get { return _score; }
            set { SetProperty(ref _score, value); }
        }

        public string SubTitle
        {
            get { return _subTitle; }
            set { SetProperty(ref _subTitle, value); }
        }


        public CalenderDailyViewModel()
        {
            _time = DateTime.Today;
            Habits = new ObservableCollection<Habit>();

            LoadHabitsCommand   = MyCommand.Create(LoadHabits);
            HabitClickedCommand = MyCommand<Habit>.Create(OnHabitSelected);
            AddHabitCommand     = MyCommand.Create(OnAddHabit);
            PreviousDayCommand  = MyCommand.Create(OnPreviousDay);
            NextDayCommand      = MyCommand.Create(OnNextDay);
            TodayCommand        = MyCommand.Create(OnGoToToday);
        }

        private void OnGoToToday(object obj)
        {
            _time = DateTime.Today;
            LoadHabits();
        }

        private void OnPreviousDay(object obj)
        {
            _time = _time.AddDays(-1);
            LoadHabits();
        }

        private void OnNextDay(object obj)
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
            Title    = _time.ToString("MMMM") + " " + _time.Day.ToString(Culture);
            SubTitle = _time.ToString("dddd");
            Score    = GetScore();
        }

        private string GetScore()
        {
            var habits = GetHabits()?.ToList();

            if(habits == null || habits.Count == 0)
                return string.Empty;

            var scoreInteger = habits.Sum(h => h.Score);
            var scoreString  = scoreInteger.ToString(Culture);

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
