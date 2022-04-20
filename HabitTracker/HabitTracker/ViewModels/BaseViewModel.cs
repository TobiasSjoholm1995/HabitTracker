﻿using HabitTracker.Models;
using HabitTracker.Services;
using System;
using Xamarin.Forms;

namespace HabitTracker.ViewModels
{
    public class BaseViewModel : NotifyPropertyChanged
    {
        protected readonly IHabits AllHabits;
        protected readonly IHabits CompletedHabits;

        public BaseViewModel()
        {
            var facade = DependencyService.Get<IHabitFacade>();

            if (facade == null)
                throw new Exception("Unable to load data");

            AllHabits = facade.All();
            CompletedHabits = facade.Completed();
        }


        bool isBusy = false;
        public bool IsBusy
        {
            get { return isBusy; }
            set { SetProperty(ref isBusy, value); }
        }

        string title = string.Empty;
        public string Title
        {
            get { return title; }
            set { SetProperty(ref title, value); }
        }

    }
}
