using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Diagnostics;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string inputText = InputTextBox.Text;
            if (inputText.Length == 0) return;
            TextListBox.Items.Add(inputText);
            // MessageBox.Show("Button clicked!");

            MenuItem newMenuItem = new MenuItem
            {
                Header = inputText
            };

            MainMenu.Items.Add(newMenuItem);
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Button clicked!");
        }

        // private void MenuItem_Click(object sender, RoutedEventArgs e)
        // {
        //     // MessageBox.Show("Menu item clicked!");
        //     string menuItemText = InputTextBox.Text;
        //
        //     if (menuItemText.Length == 0) return;
        //     MenuItem newMenuItem = new MenuItem
        //     {
        //         Header = menuItemText
        //     };
        // }

        private void MenuItem_PreviewMouseLeftButtonDown (object sender, MouseButtonEventArgs e)
        {
            Debug.WriteLine(sender);

            if (sender is MenuItem menuItem)
            {
                menuItem.IsSubmenuOpen = !menuItem.IsSubmenuOpen;
                // Prevent further event handling to avoid default hover behavior
                e.Handled = true;
            }
        }
    }
}
