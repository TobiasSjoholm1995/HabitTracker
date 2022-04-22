using HabitTracker.Services;
using HabitTracker.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HabitTracker
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();
            DependencyService.Register<HabitsFacade>();
            MainPage = new AppShell();              
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
