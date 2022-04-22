﻿using HabitTracker.Views;
using System;
using System.Diagnostics;
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
        }

        private void OnExitClicked(object sender, EventArgs e)
        {
            Process.GetCurrentProcess().CloseMainWindow();
            //System.Diagnostics.Process.GetCurrentProcess().Kill();
        }
    }
}
