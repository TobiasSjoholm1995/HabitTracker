using HabitTracker.Models;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using static HabitTracker.Settings;

namespace HabitTracker.ViewModels
{
    class AnalyticsViewModel : BaseViewModel
    {
        public Command DeleteCommand { get; }
        public Command RefreshCommand { get; }


        private int _habitCount;

        public int HabitCount
        {
            get { return _habitCount; }
            set { SetProperty(ref _habitCount, value); }
        }


        private int _completedCount;

        public int CompletedCount
        {
            get { return _completedCount; }
            set { SetProperty(ref _completedCount, value); }
        }

        private int _completedGoodHabits;

        public int CompletedGoodHabits
        {
            get { return _completedGoodHabits; }
            set { SetProperty(ref _completedGoodHabits, value); }
        }

        private int _completedBadHabits;

        public int CompletedBadHabits
        {
            get { return _completedBadHabits; }
            set { SetProperty(ref _completedBadHabits, value); }
        }


        private string _totalScore;

        public string TotalScore
        {
            get { return _totalScore; }
            set { SetProperty(ref _totalScore, value); }
        }

        public AnalyticsViewModel()
        {
            DeleteCommand  = MyCommand.Create(async () => await OnDelete());
            RefreshCommand = MyCommand.Create(OnRefresh);
            Title          = "Analytics";
        }

        private void OnRefresh()
        {
            HabitCount = FavoriteHabits.Get().Count;
            CompletedCount = CompletedHabits.Get().Count;
            CompletedGoodHabits = CompletedHabits.Get().Where(h => h.Score > 0).Count();
            CompletedBadHabits = CompletedHabits.Get().Where(h => h.Score < 0).Count();

            var score = CompletedHabits.Get().Sum(h => h.Score);
            var scoreString = score.ToString(Culture);

            if (score > 0)
                TotalScore = "+" + scoreString;
            else
                TotalScore = scoreString;
        }

        private async Task OnDelete()
        {
            bool answer = await App.Current.MainPage.DisplayAlert("Warning", "Do you wish to delete all data?", "Yes", "No");

            if(!answer)
                return;

            await FavoriteHabits.RemoveAll();
            await CompletedHabits.RemoveAll();

            OnRefresh();
        }
    }
}
