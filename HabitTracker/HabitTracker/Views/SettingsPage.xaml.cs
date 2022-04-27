using HabitTracker.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HabitTracker.Views
{
    public partial class SettingsPage : ContentPage
    {
        public SettingsPage()
        {
            InitializeComponent();

            BindingContext = new SettingsViewModel();
        }
    }
}