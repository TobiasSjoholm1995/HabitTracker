using HabitTracker.ViewModels;
using Xamarin.Forms;

namespace HabitTracker.Views
{
    public partial class FavoriteHabitsPage : ContentPage
    {
        HabitsViewModel _viewModel;

        public FavoriteHabitsPage()
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