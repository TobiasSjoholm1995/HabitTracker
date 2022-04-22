using HabitTracker.ViewModels;
using Xamarin.Forms;

namespace HabitTracker.Views
{
    public partial class CalenderDailyPage : ContentPage
    {
        CalenderDailyViewModel _viewModel;

        public CalenderDailyPage()
        {
            InitializeComponent();

            BindingContext = _viewModel = new CalenderDailyViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }
    }
}