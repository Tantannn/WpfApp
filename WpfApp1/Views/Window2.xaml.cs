using System.Data;
using System.Diagnostics;
using System.Net.Http;
using System.Windows;
using Newtonsoft.Json;
using WpfApp1.ViewModels;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace WpfApp1.Views;

public partial class Window2 : System.Windows.Window
{
    public Window2()
    {
        InitializeComponent();
    }

    private void Button_Page1(object sender, RoutedEventArgs e)
    {
    }

    private async void Button_Page2(object sender, RoutedEventArgs e)
    {
        try
        {
            var data = await FetchApiDataAsync("https://dummyapi.online/api/movies");
            // var currentDataTable = JsonConvert.DeserializeObject<DataTable>(data);
            DataGrid.ItemsSource = data;
            if (data != null)
                foreach (var movie in data)
                {
                    new List<MovieData>().Add(movie);
                }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error fetching data: {ex.Message}");
        }
    }

    private async Task<List<MovieData>?> FetchApiDataAsync(string url)
    {
        using var client = new HttpClient();
        var res = await client.GetAsync(url);
        var jsonResponse = await res.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<List<MovieData>>(jsonResponse);
    }
}
