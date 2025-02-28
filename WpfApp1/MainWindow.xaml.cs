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
using System.Media;
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

        Dictionary<string, int[]> scales = new Dictionary<string, int[]>
        {
            { "C Major", new int[] { 2, 2, 1, 2, 2, 2, 1 } }, // Whole, Whole, Half, Whole, Whole, Whole, Half
            { "A Minor", new int[] { 2, 1, 2, 2, 1, 2, 2 } }, // Whole, Half, Whole, Whole, Half, Whole, Whole
            { "Chromatic", new int[] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 } } // All half steps
        };

        Dictionary<string, int> noteToIndex = new Dictionary<string, int>
        {
            { "C", 0 },
            { "C#", 1 },
            { "D", 2 },
            { "D#", 3 },
            { "E", 4 },
            { "F", 5 },
            { "F#", 6 },
            { "G", 7 },
            { "G#", 8 },
            { "A", 9 },
            { "A#", 10 },
            { "B", 11 }
        };

        Dictionary<int, string> indexToNote = new Dictionary<int, string>
        {
            { 0, "C" },
            { 1, "C#" },
            { 2, "D" },
            { 3, "D#" },
            { 4, "E" },
            { 5, "F" },
            { 6, "F#" },
            { 7, "G" },
            { 8, "G#" },
            { 9, "A" },
            { 10, "A#" },
            { 11, "B" }
        };

        private void PlayNote(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            if (button != null)
            {
                string soundFile = button.Tag as string;
                if (!string.IsNullOrEmpty(soundFile))
                {
                    try
                    {
                        var player = new SoundPlayer($"Sounds/{soundFile}");
                        player.Play();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error playing sound: {ex.Message}");
                    }
                }
            }
        }

        List<string> GenerateScale(string rootNote, string scaleName)
        {
            List<string> scaleNotes = new List<string>();
            if (scales.ContainsKey(scaleName) && noteToIndex.ContainsKey(rootNote))
            {
                int[] intervals = scales[scaleName];
                int currentIndex = noteToIndex[rootNote];

                scaleNotes.Add(rootNote);
                foreach (int interval in intervals)
                {
                    currentIndex = (currentIndex + interval) % 12; // Wrap around using modulo 12
                    scaleNotes.Add(indexToNote[currentIndex]);
                }
            }

            return scaleNotes;
        }

        private void GenerateScale_Click(object sender, RoutedEventArgs e)
        {
            var selectedScale = (ScaleSelector.SelectedItem as ComboBoxItem)?.Content.ToString();
            var rootNote = "C"; // You can make this dynamic with another ComboBox

            List<string> scaleNotes = GenerateScale(rootNote, selectedScale);
            ScaleNotesDisplay.Text = string.Join(", ", scaleNotes);
            HighlightScaleKeys(scaleNotes);
        }

        private void HighlightScaleKeys(List<string> scaleNotes)
        {
            foreach (Button key in PianoKeys.Children)
            {
                if (scaleNotes.Contains(key.Content.ToString()))
                {
                    key.Background = Brushes.LightGreen; // Highlight keys in the scale
                }
                else
                {
                    key.Background = Brushes.White; // Reset other keys
                }
            }
        }
    }
}