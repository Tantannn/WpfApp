using System.Data;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Win32;
using Newtonsoft.Json;

namespace WpfApp1.Views.Frames;

public partial class Page1 : Page
{
    private DataTable? _currentDataTable;

    public Page1()
    {
        InitializeComponent();
    }

    private void GoToPage2_Click(object sender, RoutedEventArgs e)
    {
        this.NavigationService?.Navigate(new Page2());
    }

    private void Openfile(object sender, RoutedEventArgs e)
    {
        var openFileDialog = new OpenFileDialog()
        {
            Filter = "JSON Files (*.json)|*.json|All Files (*.*)|*.*"
        };
        if (openFileDialog.ShowDialog() == true)
        {
            var filePath = openFileDialog.FileName;

            // Read the file and parse JSON data
            var jsonData = File.ReadAllText(filePath);
            _currentDataTable = JsonConvert.DeserializeObject<DataTable>(jsonData);
            DataGrid.ItemsSource = _currentDataTable?.DefaultView;
        }
    }

    private void Savefile(object sender, RoutedEventArgs e)
    {
        var saveFileDialog = new SaveFileDialog()
        {
            Filter = "JSON Files (*.json)|*.json|All Files (*.*)|*.*"
        };
        if (_currentDataTable == null) return;
        if (saveFileDialog.ShowDialog() == true)
        {
            var filePath = saveFileDialog.FileName;

            // Serialize the DataTable to JSON
            var jsonData = JsonConvert.SerializeObject(_currentDataTable, Formatting.Indented);

            // Save the JSON data to the file
            File.WriteAllText(filePath, jsonData);

            MessageBox.Show("File saved successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
