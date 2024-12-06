using System.Windows;
using System.Windows.Controls;

namespace WpfApp1.Frames;

public partial class Page2 : Page
{
    public Page2()
    {
        InitializeComponent();
    }

    private void GoToPage1_Click(object sender, RoutedEventArgs e)
    {
        this.NavigationService?.Navigate(new Page1());
    }
}