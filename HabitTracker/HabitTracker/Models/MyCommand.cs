using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace HabitTracker.Models
{
    static class MyCommand
    {

        public static Command Create(Action a)
        {
            return new Command(SafeExecution(a));
        }

        public static Command Create(Action<object> a)
        {
            return new Command(SafeExecution(a));
        }

        public static async Task OnError(Exception ex)
        {
            await App.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
        }

        private static Action SafeExecution(Action a)
        {
            return async () =>
            {
                try
                {
                    a();
                }
                catch (Exception ex)
                {
                    await OnError(ex);
                }
            };
        }

        private static Action<object> SafeExecution(Action<object> a)
        {
            return async (o) =>
            {
                try
                {
                    a(o);
                }
                catch (Exception ex)
                {
                    await OnError(ex);
                }
            };
        }
    }

    static class MyCommand<T>
    {

        public static Command<T> Create(Action<T> a)
        {
            return new Command<T>(SafeExecution(a));
        }

        private static Action<T> SafeExecution(Action<T> a)
        {
            return async (o) =>
            {
                try
                {
                    a(o);
                }
                catch (Exception ex)
                {
                    await MyCommand.OnError(ex);
                }
            };
        }
    }
}
