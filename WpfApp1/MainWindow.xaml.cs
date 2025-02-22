using System.Collections.ObjectModel;
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
using WpfApp1.ViewModels;
using WpfApp1.Views;
using HelixToolkit.Wpf;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : System.Windows.Window
    {
        public MainWindow()
        {
            InitializeComponent();
            CustomInputControlInstance.TextSubmitted += CustomInputControl_TextSubmitted!;
        }

        private void CustomInputControl_TextSubmitted(object sender, string inputText)
        {
            // Add the submitted text to the ListBox
            TextListBox.Items.Add(inputText);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var inputText = InputTextBox.Text;
            if (inputText.Length == 0) return;
            TextListBox.Items.Add(inputText);
            // MessageBox.Show("Button clicked!");

            var newMenuItem = new MenuItem
            {
                Header = inputText
            };

            MainMenu.Items.Add(newMenuItem);
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {

            var window2 = new Window2();
            window2.Show();
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

        private void MenuItem_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Debug.WriteLine(sender.ToString());

            if (sender is MenuItem menuItem)
            {
                menuItem.IsSubmenuOpen = !menuItem.IsSubmenuOpen;
                // Prevent further event handling to avoid default hover behavior
                e.Handled = true;
            }
        }
        // https://dummyapi.online/api/movies
        private void What_Button(object sender, RoutedEventArgs e)
        {
            var window1 = new Window1();
            window1.Show();
        }
        
        private void ComboBox1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ComboBox1.SelectedItem is ComboBoxItem selectedItem)
            {
                var selectedValue = selectedItem.Content.ToString();
                TextListBox.Items.Clear();
                TextListBox.Items.Add(selectedValue);
            }
        }

        private void Open_3D_Stuffs(object sender, RoutedEventArgs e)
        {
            var window3 = new Window3();
            window3.Show();
        }

        private void PlayNote(object sender, RoutedEventArgs e)
        {
            
        }
    }
}

