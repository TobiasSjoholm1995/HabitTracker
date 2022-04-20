using HabitTracker.Models;
using HabitTracker.ViewModels;
using Xamarin.Forms;

namespace HabitTracker.Views
{
    public partial class NewHabitPage : ContentPage
    {
        public NewHabitPage()
        {
            InitializeComponent();
            BindingContext = new NewHabitViewModel();
        }
    }
}