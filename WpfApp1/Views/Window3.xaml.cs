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
