using HabitTracker.Models;
using System;
using System.Globalization;
using Xamarin.Forms;

namespace HabitTracker.ViewModels
{
    public class NewHabitViewModel : BaseViewModel
    {
        private string _name;
        private string _score;

        public Command SaveCommand { get; }
        public Command CancelCommand { get; }

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


        public NewHabitViewModel()
        {
            SaveCommand = new Command(OnSave, ValidateSave);
            CancelCommand = new Command(OnCancel);
            PropertyChanged += (_, __) => SaveCommand.ChangeCanExecute();
        }


        private bool ValidateSave()
        {
            return !string.IsNullOrWhiteSpace(_name)
                && int.TryParse(_score, out _);
        }

        private async void OnCancel()
        {
            var currentPage = "..";
            await Shell.Current.GoToAsync(currentPage);
        }

        private async void OnSave()
        {
            var habit = new Habit()
            {
                Name  = Name.Replace(Environment.NewLine, string.Empty),
                Score = int.Parse(Score, NumberStyles.Any, CultureInfo.InvariantCulture)
            };

            await AllHabits.Add(habit);

            var currentPage = "..";
            await Shell.Current.GoToAsync(currentPage);
        }
    }
}
