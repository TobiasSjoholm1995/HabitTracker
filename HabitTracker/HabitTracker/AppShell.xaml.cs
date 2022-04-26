﻿using HabitTracker.Views;
using System;
using System.Diagnostics;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace HabitTracker
{

    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(HabitDetailPage), typeof(HabitDetailPage));
            Routing.RegisterRoute(nameof(NewHabitPage), typeof(NewHabitPage));
            Routing.RegisterRoute(nameof(HabitDetailPage), typeof(HabitDetailPage));
            Routing.RegisterRoute(nameof(HabitDetailCalenderPage), typeof(HabitDetailCalenderPage));
            Routing.RegisterRoute(nameof(SelectHabitPage), typeof(SelectHabitPage));
            Routing.RegisterRoute(nameof(CalenderWeeklyPage), typeof(CalenderWeeklyPage));
            Routing.RegisterRoute(nameof(CalenderDailyPage), typeof(CalenderDailyPage));
            Routing.RegisterRoute(nameof(AnalyticsPage), typeof(AnalyticsPage));

            GoToStartPage();
        }

        private void GoToStartPage()
        {
            if (Settings.IsFirstRun)
            {
                CurrentItem = Info;
                Settings.IsFirstRun = false;
            }
            else
            {
                CurrentItem = Calender;
            }
        }

        private void OnExitClicked(object sender, EventArgs e)
        {
            Process.GetCurrentProcess().CloseMainWindow();
            //System.Diagnostics.Process.GetCurrentProcess().Kill();
        }
    }
}
