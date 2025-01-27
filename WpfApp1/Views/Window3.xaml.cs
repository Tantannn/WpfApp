using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using HelixToolkit.Wpf;
using Microsoft.Win32;
namespace WpfApp1.Views;

public partial class Window3 : System.Windows.Window
{
    public Window3()
    {
        InitializeComponent();
        // LoadModelCommand = new RelayCommand(LoadModelButton_Click);
        // Load3dModel();
    }

    private void Load3dModel()
    {
        var currentHelixObjReader = new ObjReader();
        // Model3DGroup MyModel = CurrentHelixObjReader.Read(@"D:\3DModel\dinosaur_FBX\dinosaur.fbx");
        // Model3DGroup MyModel = currentHelixObjReader.Read(@"D:\Download\ply\eyeball.ply");
        // model.Content = MyModel;
        //MyModel.Children.Add(MyModel);

        // HelixViewport3D.Children.Add(MyModel); // Add the new model

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