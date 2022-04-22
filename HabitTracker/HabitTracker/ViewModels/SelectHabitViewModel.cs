﻿using HabitTracker.Models;
using System;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using static HabitTracker.Constants;

namespace HabitTracker.ViewModels
{
    [QueryProperty(nameof(Time), nameof(Time))]

    class SelectHabitViewModel : BaseViewModel
    {
        private Habit _selected;

        public ObservableCollection<Habit> Habits { get; }
        public Command LoadHabitsCommand { get; }
        public Command<Habit> HabitClickedCommand { get; }

        public SelectHabitViewModel()
        {
            Title = "Select Habit";
            Habits = new ObservableCollection<Habit>();

            LoadHabitsCommand   = new Command(LoadHabits);
            HabitClickedCommand = new Command<Habit>(OnHabitSelected);
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

        public string Time { get; set; }

        async void OnHabitSelected(Habit item)
        {
            if (item == null)
                return;

            if(Time == null)
                return;

            var habit = new Habit
            {
                Name = item.Name,
                Score = item.Score,
                Date = DateTime.ParseExact(Time, ViewDateFormat, Culture)
            };

            await CompletedHabits.Add(habit);
            await GoToPreviousPage();
        }
    }
}
