using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace WpfApp1.Views.Frames;

public partial class Page2 : Page
{
    public Page2()
    {
        InitializeComponent();
        var timer = new DispatcherTimer
        {
            Interval = TimeSpan.FromMilliseconds(1000) // Update every second
        };
        timer.Tick += Timer_Tick!;
        timer.Start();
    }

    private void GoToPage1_Click(object sender, RoutedEventArgs e)
    {
        NavigationService?.Navigate(new Page1());
    }

    private void Timer_Tick(object sender, EventArgs e)
    {
        var pc = new PerformanceCounter("Memory", "Available MBytes");
        var cpuUsage = pc.NextValue();
        CpuUsageTextBlock.Text = $"Total RAM: {cpuUsage:0.00}  MB";
    }
}