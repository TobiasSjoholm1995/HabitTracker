using HabitTracker.Models;
using System;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using static HabitTracker.Settings;

namespace HabitTracker.ViewModels
{
    [QueryProperty(nameof(ID), nameof(ID))]
    class HabitDetailCalenderViewModel : BaseViewModel
    {
        private string _id;
        private string _name;
        private string _date;
        private string _score;
        private Habit _habit;

        public Command DeleteHabitCommand { get; }


        public HabitDetailCalenderViewModel()
        {
            Title = "Completed Habit";
            DeleteHabitCommand = MyCommand.Create(async () => await OnDeleteHabit());
        }

        private async Task OnDeleteHabit()
        {
            await CompletedHabits.Remove(_habit);
            await GoToPreviousPage();
        }

        public string Id { get; set; }

        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        public string Date
        {
            get => _date;
            set => SetProperty(ref _date, value);
        }

        public string Score
        {
            get => _score;
            set => SetProperty(ref _score, value);
        }

        public string ID
        {
            get
            {
                return _id;
            }
            set
            {
                _id = value;
                LoadHabit(value);
            }
        }

        public void LoadHabit(string id)
        {
            _habit = CompletedHabits.Get().Where(h => string.Equals(h.Id, id, StringComparison.InvariantCulture)).FirstOrDefault();

            if (_habit == null)
                throw new Exception("Failed to Load habit");

            Name = _habit.Name;
            Date = _habit.Date.ToString(ViewDateFormat, Culture);
            Score = _habit.Score.ToString(CultureInfo.InvariantCulture);
        }
    }
}

