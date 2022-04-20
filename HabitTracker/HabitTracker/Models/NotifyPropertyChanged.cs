using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace HabitTracker.Models
{
    public class NotifyPropertyChanged : INotifyPropertyChanged
    {

        protected bool SetProperty<T>(ref T variable, T value, [CallerMemberName] string propertyName = "")
        {
            if (EqualityComparer<T>.Default.Equals(variable, value))
                return false;

            variable = value;
            OnPropertyChanged(propertyName);
            return true;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;

            changed?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
