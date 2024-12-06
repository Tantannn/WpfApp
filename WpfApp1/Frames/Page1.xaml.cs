using System.Data;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Win32;
using Newtonsoft.Json;

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

    private void Openfile(object sender, RoutedEventArgs e)
    {
        OpenFileDialog openFileDialog = new OpenFileDialog()
        {
            Filter = "JSON Files (*.json)|*.json|All Files (*.*)|*.*"
        };
        if (openFileDialog.ShowDialog() == true)
        {
            var filePath = openFileDialog.FileName;

            // Read the file and parse JSON data
            var jsonData = File.ReadAllText(filePath);
            DataTable? dataTable = JsonConvert.DeserializeObject<DataTable>(jsonData);
            DataGrid.ItemsSource = dataTable?.DefaultView;
        }
    }

    private void Savefile(object sender, RoutedEventArgs e)
    {
        
    }
}