using HabitTracker.ViewModels;
using Xamarin.Forms;

namespace HabitTracker.Views
{
    public partial class SelectHabitPage : ContentPage
    {
        private SelectHabitViewModel _viewModel;

        public SelectHabitPage()
        {
            InitializeComponent();

            BindingContext = _viewModel = new SelectHabitViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }
    }
}