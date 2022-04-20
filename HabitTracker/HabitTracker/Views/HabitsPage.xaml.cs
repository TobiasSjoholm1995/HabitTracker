using HabitTracker.ViewModels;
using Xamarin.Forms;

namespace HabitTracker.Views
{
    public partial class HabitsPage : ContentPage
    {
        HabitViewModel _viewModel;

        public HabitsPage()
        {
            InitializeComponent();

            BindingContext = _viewModel = new HabitViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }
    }
}