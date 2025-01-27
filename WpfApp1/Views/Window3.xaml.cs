using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using HelixToolkit.SharpDX.Core.Model.Scene;
using HelixToolkit.Wpf;
using HelixToolkit.Wpf.SharpDX;
using Microsoft.Win32;
using DiffuseMaterial = System.Windows.Media.Media3D.DiffuseMaterial;
using GeometryModel3D = System.Windows.Media.Media3D.GeometryModel3D;

namespace WpfApp1.Views;

public partial class Window3 : System.Windows.Window
{
    public Window3()
    {
        InitializeComponent();
        // LoadModelCommand = new RelayCommand(LoadModelButton_Click);
        // Load3dModel();
    }

        private void LoadModel()
        {
            var openFileDialog = new OpenFileDialog
            {
                Title = "Select a 3D Model",
                Filter = "3D Model Files (*.obj;*.fbx)|*.obj;*.fbx|All Files (*.*)|*.*"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                var reader = new HelixToolkit.Wpf.SharpDX.StudioReader();
                var modelGroup = reader.Read(openFileDialog.FileName);

                foreach (var model in modelGroup)
                {
                    var sharpModel = new MeshGeometryModel3D
                    {
                        Geometry = model.Geometry,
                        Material = model.Material,
                        Name = model.Name,
                        Transform = new MatrixTransform3D(model.Transform.ToMatrix3D())
                    };

                    HelixViewport3D.Children.Add(sharpModel);
                }
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
            var filePath = openFileDialog.FileName;

            // Use ModelImporter to load the selected file
            var importer = new ModelImporter();
            try
            {
                var model = importer.Load(filePath);
                var material = new DiffuseMaterial(new SolidColorBrush(Colors.Gray));
                foreach (var geometry in model.Children.OfType<ModelVisual3D>())
                {
                    if (geometry.Content is GeometryModel3D geometryModel)
                    {
                        geometryModel.Material = material;
                    }
                }
                HelixViewport3D.Children.Clear(); // Clear previous models
                var modelVisual = new ModelVisual3D
                {
                    Content = model
                };
                HelixViewport3D.Children.Add(modelVisual); // Add the new model
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading model: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}