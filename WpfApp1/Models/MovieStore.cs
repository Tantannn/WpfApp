using System.Collections.ObjectModel;
using System.ComponentModel;
using WpfApp1.ViewModels;

public class MovieStore: INotifyPropertyChanged
{
    private ObservableCollection<MovieData> _movies = new ObservableCollection<MovieData>();
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
