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
    class CalenderWeeklyViewModel : BaseViewModel
    {
        private Habit _selected;
        private DateTime _time;
        private string _score;
        private string _subTitle;

        public ObservableCollection<Habit> Habits { get; }
        public Command LoadHabitsCommand { get; }
        public Command<Habit> HabitClickedCommand { get; }
        public Command PreviousWeekCommand { get; }
        public Command NextWeekCommand { get; }
        public Command TodayCommand { get; }


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

        public CalenderWeeklyViewModel()
        {
            _time = DateTime.Today;
            Habits = new ObservableCollection<Habit>();

            LoadHabitsCommand    = new Command(LoadHabits);
            HabitClickedCommand  = new Command<Habit>(OnHabitSelected);
            PreviousWeekCommand  = new Command(OnPreviousWeek);
            NextWeekCommand      = new Command(OnNextWeek);
            TodayCommand         = new Command(OnGoToToday);
        }

        private void OnGoToToday(object obj)
        {
            _time = DateTime.Today;
            LoadHabits();
        }

        private void OnPreviousWeek(object obj)
        {
            _time = _time.AddDays(-7);
            LoadHabits();
        }

        private void OnNextWeek(object obj)
        {
            _time = _time.AddDays(7);
            LoadHabits();
        }

        private IEnumerable<Habit> GetHabits(DateTime time)
        {
            foreach (var habit in CompletedHabits.Get())
                if (habit.Date.Date == time.Date)
                    yield return habit;
        }

        private void SetTitle()
        {
            Title    = _time.Year.ToString(Culture);
            SubTitle = "Week " + GetWeekNr(_time);
            Score    = GetScore();
        }

        private string GetScore()
        {
            List<Habit> habits = new List<Habit>();

            for (int i = StartOfWeek; i < StartOfWeek + 7; i++)
            {
                var day  = GetDay(_time, i);
                habits.AddRange(GetHabits(day));
            }

            if(habits == null || habits.Count == 0)
                return string.Empty;

            var scoreInteger = habits.Sum(h => h.Score);
            var scoreString  = scoreInteger.ToString(Culture);

            if (scoreInteger > 0)
                return "+" + scoreString;

            return scoreString;
        }

        public int GetWeekNr(DateTime time)
        {
            return Culture.Calendar.GetWeekOfYear(time, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
        }

        public DateTime GetDay(DateTime time, int day)
        {
            int diff = (7 + ((int)time.DayOfWeek - StartOfWeek)) % 7;
            return time.AddDays(-diff + day - StartOfWeek);
        }

        void LoadHabits()
        {
            IsBusy = true;

            try
            {
                Habits.Clear();

                for (int i = StartOfWeek; i < StartOfWeek + 7; i++)
                {
                    var day    = GetDay(_time, i);
                    var habits = GetHabits(day).ToList();
                    var item   = new Habit
                    {
                        Name  = day.ToString("dddd"),
                        Score = habits.Sum(h => h.Score),
                        Date  = day
                    };

                    Habits.Add(item);
                }

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

        async void OnHabitSelected(Habit item)
        {
            if (item == null)
                return;

            await Shell.Current.GoToAsync($"{nameof(CalenderDailyPage)}?{nameof(CalenderDailyViewModel.Time)}={item.Date.ToString(ViewDateFormat)}", true);
        }

    }
}
