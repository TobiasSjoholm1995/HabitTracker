using System;
using Xamarin.Forms;
using System.Globalization;
using HabitTracker.Models;
using static HabitTracker.Settings;

namespace HabitTracker.ViewModels
{

    [QueryProperty(nameof(Time), nameof(Time))]
    public class NewHabitViewModel : BaseViewModel
    {
        private string _name;
        private string _score;


        public Command SaveCommand { get; }
        public Command CancelCommand { get; }
        public string Time { get; set; }

        public bool AddToCompleted
        {
            get => !string.IsNullOrEmpty(Time);
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

            await AllHabits.Add(habit);

            if(AddToCompleted)
            {
                var completedHabit = new Habit
                {
                    Name  = habit.Name,
                    Score = habit.Score,
                    Date  = DateTime.ParseExact(Time, ViewDateFormat, Culture)
                };

                await CompletedHabits.Add(completedHabit);
                await GoToPreviousPage();
            }

            await GoToPreviousPage();
        }
    }
}
