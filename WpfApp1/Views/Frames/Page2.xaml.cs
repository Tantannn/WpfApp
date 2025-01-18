using System.Diagnostics;
using HelixToolkit.Wpf;
using System.Windows.Media.Media3D;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Threading;

namespace WpfApp1.Views.Frames;

public partial class Page2 : Page
{
    public Page2()
    {
        InitializeComponent();
        InitializeRotation();
        var timer = new DispatcherTimer
        {
            Interval = TimeSpan.FromMilliseconds(1000) // Update every second
        };
        timer.Tick += Timer_Tick!;
        timer.Start();
    }
    private static readonly AxisAngleRotation3D Rotation = new AxisAngleRotation3D(new Vector3D(0, 1, 0), 0);
    private readonly RotateTransform3D _rotateTransform  = new RotateTransform3D(Rotation);

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
    
            private void InitializeRotation()
            {
                // Create rotation components
                // _rotation
                // _rotateTransform
    
                // Apply the rotation to the BoxVisual3D
                BoxVisual.Transform = _rotateTransform;
    
                // Start a timer to animate the rotation
                var timer = new System.Windows.Threading.DispatcherTimer
                {
                    Interval = TimeSpan.FromMilliseconds(30) // Update every 30ms
                };
                timer.Tick += (s, e) => AnimateRotation();
                timer.Start();
            }
    
            private void AnimateRotation()
            {
                Rotation.Angle += 2; // Increment the angle
                if (Rotation.Angle >= 360) Rotation.Angle = 0; // Reset after full rotation
            }
}