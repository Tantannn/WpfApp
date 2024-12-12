using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using WpfApp1.ViewModels;

public class MovieStore: INotifyPropertyChanged
{
    private ObservableCollection<MovieData> _movies = [];
    public ObservableCollection<MovieData> Movies
    {
        get => _movies;
        set
        {
            _movies = value;
            OnPropertyChanged(nameof(Movies));
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
