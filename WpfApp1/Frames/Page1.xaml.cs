using System.Windows;
using System.Windows.Controls;

namespace WpfApp1.Frames;

public partial class Page1 : Page
{
    public Page1()
    {
        InitializeComponent();
    }

    private void GoToPage2_Click(object sender, RoutedEventArgs e)
    {
        this.NavigationService?.Navigate(new Page2());

    }
}