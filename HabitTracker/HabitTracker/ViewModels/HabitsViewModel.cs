using HabitTracker.Models;
using HabitTracker.Services;
using HabitTracker.Views;
using System;
using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace HabitTracker.ViewModels
{
    public class HabitsViewModel : BaseViewModel
    {
        private Habit _selected;

        public ObservableCollection<Habit> Habits { get; }
        public Command LoadHabitsCommand { get; }
        public Command AddHabitCommand { get; }
        public Command<Habit> HabitClickedCommand { get; }

        public HabitsViewModel()
        {
            Title = "Habits";
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