using GismeteoPresentation.Extension;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace GismeteoPresentation
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected bool SetProperty<T>(ref T field, T value, [CallerMemberName] string propertyName = "")
        {
            if (Equals(field, value))
                return false;
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }

        private void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (propertyName.IsEmpty())
                throw new WarningException("propertyName is empty");

            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
