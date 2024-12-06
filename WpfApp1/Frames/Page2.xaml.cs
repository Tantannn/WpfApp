using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace WpfApp1.Frames;

public partial class Page2 : Page
{
    public Page2()
    {
        InitializeComponent();
        DispatcherTimer timer = new DispatcherTimer
        {
            Interval = TimeSpan.FromMilliseconds(1000) // Update every second
        };
        timer.Tick += Timer_Tick;
        timer.Start();
    }

    private void GoToPage1_Click(object sender, RoutedEventArgs e)
    {
        this.NavigationService?.Navigate(new Page1());
    }

    private void Timer_Tick(object sender, EventArgs e)
    {
       var pc = new PerformanceCounter ("Memory", "Available MBytes");
        // Debug.WriteLine(pc.NextValue());
        // Get CPU usage and update the TextBlock
        float cpuUsage = pc.NextValue();
        CpuUsageTextBlock.Text = $"Total RAM: {cpuUsage:0.00}  MB\\n\\n";
    }
}
