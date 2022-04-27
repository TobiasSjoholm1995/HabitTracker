using System;
using Xamarin.Forms;
using System.Globalization;
using HabitTracker.Models;
using static HabitTracker.Settings;

namespace HabitTracker.ViewModels
{

    [QueryProperty(nameof(DateAndTime), nameof(DateAndTime))]
    public class NewHabitViewModel : BaseViewModel
    {
        private string _name;
        private string _score;
        private bool _isDateEnabled;
        private string _dateAndTime;
        private DateTime _date;


        public Command SaveCommand { get; }
        public Command CancelCommand { get; }

        public bool IsDateEnabled { 
            get { return _isDateEnabled; }
            set { SetProperty(ref _isDateEnabled, value); }
        }

        public string DateAndTime
        {
            get
            {
                return _dateAndTime;
            }
            set
            {
                _dateAndTime = value;
                Date = DateTime.ParseExact(value, ViewDateFormat, Culture, DateTimeStyles.AssumeUniversal);
                IsDateEnabled = !string.IsNullOrEmpty(value);
            }
        }

        public string DateFormat { get => ViewDateFormat; }

        public DateTime Date
        {
            get { return _date; }
            // GUI don't show time, so when user configure the date it will overwrite the time part 
            // so use _time variable for time
            set { SetProperty(ref _date, value); } 
        }

        public bool AddToCompleted
        {
            get => !string.IsNullOrEmpty(DateAndTime);
        }

        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        public string Score
        {
            get => _score;
            set => SetProperty(ref _score, value);
        }

        private string _error;
        public string Error
        {
            get => _error;
            set => SetProperty(ref _error, value);
        }


        public NewHabitViewModel()
        {
            Score = "1";
            Error = string.Empty;
            SaveCommand = MyCommand.Create(OnSave, ValidateSave);
            CancelCommand = MyCommand.Create(OnCancel);
            PropertyChanged += (_, __) => SaveCommand.ChangeCanExecute();
        }

        private bool ValidateSave()
        {
            if(!string.IsNullOrEmpty(_name) && _name.Contains(Separator.ToString(Culture)))
            {
                Error = "Name has invalid characters";
                return false;
            }

            if (!string.IsNullOrEmpty(_score) 
                && !string.Equals(_score, "-", StringComparison.InvariantCultureIgnoreCase) 
                && !int.TryParse(_score, out _))
            {
                Error = "Score must be a integer";
                return false;
            }

            Error = string.Empty;

            return !string.IsNullOrWhiteSpace(_name) && int.TryParse(_score, out _);
        }

        private async void OnCancel()
        {
            await GoToPreviousPage();
        }

        private async void OnSave()
        {
            var habit = new Habit()
            {
                Name  = Name.Replace(Environment.NewLine, string.Empty),
                Score = int.Parse(Score, NumberStyles.Any, CultureInfo.InvariantCulture)
            };


            if(AddToCompleted)
            {
                if(IsAutoAddToFavoritesEnabled)
                    await FavoriteHabits.Add(habit);

                var completedHabit = new Habit
                {
                    Name  = habit.Name,
                    Score = habit.Score,
                    Date  = Date.Add(DateTime.Now.TimeOfDay)
                };

                await CompletedHabits.Add(completedHabit);
                await GoToPreviousPage();
            }
            else
            {
                await FavoriteHabits.Add(habit);
            }

            await GoToPreviousPage();
        }
    }
}
