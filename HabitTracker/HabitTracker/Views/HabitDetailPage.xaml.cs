using HabitTracker.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace HabitTracker.Views
{
    public partial class HabitDetailPage : ContentPage
    {
        public HabitDetailPage()
        {
            InitializeComponent();
            BindingContext = new HabitDetailViewModel();
        }
    }
}