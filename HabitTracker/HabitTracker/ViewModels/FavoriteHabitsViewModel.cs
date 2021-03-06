using HabitTracker.Models;
using HabitTracker.Services;
using HabitTracker.Views;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using Xamarin.Forms;

namespace HabitTracker.ViewModels
{
    public class FavoriteHabitsViewModel : BaseViewModel
    {
        private Habit _selected;

        public ObservableCollection<Habit> Habits { get; }
        public Command LoadHabitsCommand { get; }
        public Command AddHabitCommand { get; }
        public Command<Habit> HabitClickedCommand { get; }

        public FavoriteHabitsViewModel()
        {
            Title = "Favorite Habits";
            Habits = new ObservableCollection<Habit>();

            LoadHabitsCommand   = MyCommand.Create(LoadHabits);
            HabitClickedCommand = MyCommand<Habit>.Create(OnHabitSelected);
            AddHabitCommand     = MyCommand.Create(OnAddHabit);
        }

        void LoadHabits()
        {
            IsBusy = true;

            try
            {
                Habits.Clear();
                foreach (var habit in FavoriteHabits.Get().OrderBy(h => h.Name))
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
            await Shell.Current.GoToAsync($"{nameof(FavoriteHabitDetailPage)}?{nameof(HabitDetailViewModel.ID)}={item.Id}");
        }
    }
}