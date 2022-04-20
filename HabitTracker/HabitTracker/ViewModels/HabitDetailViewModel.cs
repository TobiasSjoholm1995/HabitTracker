using HabitTracker.Services;
using System;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using Xamarin.Forms;

namespace HabitTracker.ViewModels
{

    [QueryProperty(nameof(ID), nameof(ID))]
    public class HabitDetailViewModel : BaseViewModel
    {
        private string _id;
        private string _name;
        private string _score;

        public string Id { get; set; }

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
            try
            {
                var habit = AllHabits.Get().Where(h => string.Equals(h.Id, id, StringComparison.InvariantCulture)).FirstOrDefault();

                if(habit == null)
                    throw new Exception("Failed to Load habit");

                Id    = habit.Id;
                Name  = habit.Name;
                Score = habit.Score.ToString(CultureInfo.InvariantCulture);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
    }
}
