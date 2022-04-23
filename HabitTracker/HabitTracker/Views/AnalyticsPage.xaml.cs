using HabitTracker.ViewModels;
using Xamarin.Forms;

namespace HabitTracker.Views
{
    public partial class AnalyticsPage : ContentPage
    {
        private AnalyticsViewModel _viewModel;

        public AnalyticsPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = new AnalyticsViewModel();
        }

        protected override void OnAppearing()
        {
            if(_viewModel.RefreshCommand.CanExecute(null))
                _viewModel.RefreshCommand.Execute(null);
        }
    }
}