using HabitTracker.Views;
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
            Routing.RegisterRoute(nameof(AnalyticsPage),            typeof(AnalyticsPage));
            Routing.RegisterRoute(nameof(CalenderDailyPage),        typeof(CalenderDailyPage));
            Routing.RegisterRoute(nameof(CalenderWeeklyPage),       typeof(CalenderWeeklyPage));
            Routing.RegisterRoute(nameof(CompletedHabitDetailPage), typeof(CompletedHabitDetailPage));
            Routing.RegisterRoute(nameof(FavoriteHabitDetailPage),          typeof(FavoriteHabitDetailPage));
            Routing.RegisterRoute(nameof(FavoriteHabitsPage),               typeof(FavoriteHabitsPage));
            Routing.RegisterRoute(nameof(InfoPage),                 typeof(InfoPage));
            Routing.RegisterRoute(nameof(NewHabitPage),             typeof(NewHabitPage));
            Routing.RegisterRoute(nameof(SelectHabitPage),          typeof(SelectHabitPage));

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
        }
    }
}
