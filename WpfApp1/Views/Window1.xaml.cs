using System.Windows;
using WpfApp1.Views.Frames;

namespace WpfApp1.Views;

public partial class Window1 : System.Windows.Window
{
    public Window1()
    {
        InitializeComponent();
    }

    private void Button_Page1(object sender, RoutedEventArgs e)
    {
        BottomFrame.Content = null;
        TopFrame.Navigate(new Page1());
    }

    private void Button_Page2(object sender, RoutedEventArgs e)
    {
        TopFrame.Content = null;
        BottomFrame.Navigate(new Page2());

        
    }
}