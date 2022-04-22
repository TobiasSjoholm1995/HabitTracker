using HabitTracker.ViewModels;
using Xamarin.Forms;

namespace HabitTracker.Views
{
    public partial class CalenderWeeklyPage : ContentPage
    {
        private CalenderWeeklyViewModel _viewModel;

        public CalenderWeeklyPage()
        {
            InitializeComponent();

            BindingContext = _viewModel = new CalenderWeeklyViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }
    }
}