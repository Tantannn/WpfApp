using System.Windows;
using System.Windows.Controls;

namespace WpfApp1.Views.Controls
{
    public partial class CustomInputControl : UserControl
    {
        public event EventHandler<string>? TextSubmitted; // Event to pass text to MainWindow
        public CustomInputControl()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string inputText = InputTextBox.Text;
            TextSubmitted?.Invoke(this, inputText + "hello"); // Pass the input text to the event handler
            MessageBox.Show($"You entered: {inputText}", "Input Text");
        }

    }
}