using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using HelixToolkit.Wpf;
using Microsoft.Win32;
namespace WpfApp1.Views;
public partial class Window3 : Window
{
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
        if (_modelVisual.Content == null) return;
        if (_modelVisual.Content is GeometryModel3D geometryModel)
        {
            // Create a RotateTransform3D
            var rotateTransform = new RotateTransform3D();
            var axisAngleRotation = new AxisAngleRotation3D(axis, RotationAngle);
            rotateTransform.Rotation = axisAngleRotation;
    
            // Apply the transformation
            var transformGroup = geometryModel.Transform as Transform3DGroup ?? new Transform3DGroup();
            transformGroup.Children.Add(rotateTransform);
            geometryModel.Transform = transformGroup;
        }
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
                // Load the 3D model
                Model3DGroup models = importer.Load(filePath);
                HelixViewport3D.Children.Clear(); // Clear previous models
                
                // Define a default material (red color)
                var defaultMaterial = new DiffuseMaterial(new SolidColorBrush(Colors.Red));
                
                foreach (var model in models.Children.OfType<GeometryModel3D>())
                {
                    // Assign material if it's missing
                    if (model.Material == null)
                    {
                        model.Material = defaultMaterial;
                        model.BackMaterial = defaultMaterial;
                    }
                }
                
                // Wrap models into a ModelVisual3D
                var modelVisual = new ModelVisual3D { Content = models };
                _modelVisual = modelVisual;
                HelixViewport3D.Children.Add(modelVisual);

                // Ensure there's a light source
                var light = new DirectionalLight(Colors.White, new Vector3D(-1, -1, -1));
                models.Children.Add(light);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading model: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
