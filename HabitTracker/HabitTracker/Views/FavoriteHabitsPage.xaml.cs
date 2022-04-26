using HabitTracker.ViewModels;
using Xamarin.Forms;

namespace HabitTracker.Views
{
    public partial class FavoriteHabitsPage : ContentPage
    {
        FavoriteHabitsViewModel _viewModel;

        public FavoriteHabitsPage()
        {
            InitializeComponent();

            BindingContext = _viewModel = new FavoriteHabitsViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }
    }
}