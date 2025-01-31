using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using System.Windows.Threading;
using HelixToolkit.Wpf;
using Microsoft.Win32;
namespace WpfApp1.Views;
public partial class Window3 : Window
{
    private AxisAngleRotation3D _rotation;
     private RotateTransform3D _rotateTransform;
     private DispatcherTimer _rotationTimer;
    public Window3()
    {
        InitializeComponent();
    }
     private const double RotationAngle = 10;
     private GeometryModel3D _modelVisual;
    private void RotateLeft_Click(object sender, RoutedEventArgs e)
    {
        RotateModel(new Vector3D(0, 1, 0)); // Rotate around Y-axis
    }
    private void RotateModel(Vector3D axis)
    {
        // if (_modelVisual.Content is GeometryModel3D geometryModel)
        // {
        //     _modelVisual.Transform(axis);
        //     // Create a RotateTransform3D
        //     var rotateTransform = new RotateTransform3D();
        //     var axisAngleRotation = new AxisAngleRotation3D(axis, RotationAngle);
        //     rotateTransform.Rotation = axisAngleRotation;
        //
        //     // Apply the transformation
        //     var transformGroup = geometryModel.Transform as Transform3DGroup ?? new Transform3DGroup();
        //     transformGroup.Children.Add(rotateTransform);
        //     geometryModel.Transform = transformGroup;
        // }
    }

    private void LoadModelButton_Click(object sender, RoutedEventArgs routedEventArgs)
    {
        var openFileDialog = new OpenFileDialog
        {
            Title = "Select a 3D Model",
            Filter = "3D Model Files (*.obj;*.stl;*.3ds;*.ply)|*.obj;*.stl;*.3ds;*.ply|All Files (*.*)|*.*"
        };

        if (openFileDialog.ShowDialog() == true)
        {
            string filePath = openFileDialog.FileName;
            var importer = new ModelImporter();

            try
            {
                var models = importer.Load(filePath);
                HelixViewport3D.Children.Clear();
                
                var modelVisual = new ModelVisual3D { Content = models };
                HelixViewport3D.Children.Add(modelVisual);

                var light = new DirectionalLight(Colors.White, new Vector3D(-1, -1, -1));
                models.Children.Add(light);
                // ModelContainer.Content = models;
                 StartRotation();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading model: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
    private void StartRotation()
    {
        if (_rotationTimer == null)
        {
            _rotationTimer = new DispatcherTimer
            {
                Interval = TimeSpan.FromMilliseconds(30)
            };
            _rotationTimer.Tick += (s, e) =>
            {
                if (_rotation != null) // Check before modifying
                {
                    _rotation.Angle += 2; // Rotate 2 degrees per tick
                    if (_rotation.Angle >= 360) _rotation.Angle = 0; // Reset rotation
                }
            };
            _rotationTimer.Start();
        }
        else
        {
            _rotationTimer.Start(); // Restart if already initialized
        }
    }
    private void SetupRotation()
    {
        _rotation = new AxisAngleRotation3D(new Vector3D(0, 1, 0), 0); // Rotate around Y-axis
        _rotateTransform = new RotateTransform3D(_rotation);
    }
}
