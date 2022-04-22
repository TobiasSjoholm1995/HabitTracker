using HabitTracker.ViewModels;
using Xamarin.Forms;

namespace HabitTracker.Views
{
    public partial class CalenderPage : ContentPage
    {
        CalenderViewModel _viewModel;

        public CalenderPage()
        {
            InitializeComponent();

            BindingContext = _viewModel = new CalenderViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }
    }
}