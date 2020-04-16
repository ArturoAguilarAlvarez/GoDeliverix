
using System.ComponentModel;

public class NotifyBase : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;
    public void OnpropertyChanged(string Propiedad)
    {
        if (PropertyChanged != null)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(Propiedad));
        }
    }
}

