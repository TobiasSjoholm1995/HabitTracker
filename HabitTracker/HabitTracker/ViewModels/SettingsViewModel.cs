﻿using HabitTracker.Models;
using System;
using System.Linq;
using System.Reflection;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace HabitTracker.ViewModels
{
    class SettingsViewModel : BaseViewModel
    {

        public Command ResetCommand { get; }


        public bool AutoAddHabitToFavorites
        {
            get { return Settings.IsAutoAddToFavoritesEnabled; }
            set { Settings.IsAutoAddToFavoritesEnabled = value; OnPropertyChanged(); }
        }

        public int StartOfWeek
        {
            get { return Settings.StartOfWeek; }
            set { Settings.StartOfWeek = value; OnPropertyChanged(); }
        }

        public string[] NameOfDays
        {
            get { return Enum.GetNames(typeof(DayOfWeek)); }
        }

        public int ViewDateOnlyFormat
        {
            get { return ViewDateOnlyFormats.ToList().IndexOf(Settings.ViewDateFormat); }
            set { Settings.ViewDateFormat = ViewDateOnlyFormats[value]; OnPropertyChanged(); }
        }

        public string[] ViewDateOnlyFormats
        {
            get
            {
                return new string[]
                {
                    "yyyy-MM-dd",
                    "yyyy-dd-MM",
                    "dd-MM-yyyy",
                    "dd-yyyy-MM",
                    "MM-yyyy-dd",
                    "MM-dd-yyyy",
                };
            }
        }


        public SettingsViewModel()
        {
            Title = "Settings";
            ResetCommand = MyCommand.Create(OnReset);
            StartOfWeek = Settings.StartOfWeek;
        }

        private void OnReset()
        {
            Preferences.Clear();
            
            StartOfWeek = Settings.StartOfWeek_Default;
            ViewDateOnlyFormat = ViewDateOnlyFormats.ToList().IndexOf(Settings.ViewDateFormat_Default);
            AutoAddHabitToFavorites = Settings.IsAutoAddToFavoritesEnabled_Default;
        }


    }
}

