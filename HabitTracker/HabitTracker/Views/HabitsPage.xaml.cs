using HabitTracker.ViewModels;
using Xamarin.Forms;

namespace HabitTracker.Views
{
    public partial class HabitsPage : ContentPage
    {
        HabitsViewModel _viewModel;

        public HabitsPage()
        {
            InitializeComponent();

            BindingContext = _viewModel = new HabitsViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }
    }
}