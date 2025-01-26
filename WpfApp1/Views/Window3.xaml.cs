using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Media3D;
using HelixToolkit.Wpf;
using Microsoft.Win32;
namespace WpfApp1.Views;

public partial class Window3 : System.Windows.Window
{
    public ICommand OpenFileCommand { get; }
    public Window3()
    {
        InitializeComponent();
        OpenFileCommand = new RelayCommand(LoadModelButton_Click);
        // LoadModelCommand = new RelayCommand(LoadModelButton_Click);
    }
    


    private void LoadModelButton_Click()
    {
        var openFileDialog = new OpenFileDialog
        {
            Title = "Select a 3D Model",
            Filter = "3D Model Files (*.obj;*.stl;*.3ds;*.ply)|*.obj;*.stl;*.3ds;*.ply|All Files (*.*)|*.*"
        };
    // public RelayCommand(Action execute, Func<bool> canExecute = null)
    // {
    //     _execute = execute ?? throw new ArgumentNullException(nameof(execute));
    //     _canExecute = canExecute;
    // }
        
        if (openFileDialog.ShowDialog() == true)
        {
            var filePath = openFileDialog.FileName;
        
            // Use ModelImporter to load the selected file
            var importer = new ModelImporter();
            try
            {
                var model = importer.Load(filePath);
        
                // Clear any existing children in HelixView
                HelixViewport3D.Children.Clear();
        
                // Add DefaultLights for better visibility
                HelixViewport3D.Children.Add(new DefaultLights());
        
                // Create a ModelVisual3D and set the loaded model as its content
                var modelVisual = new ModelVisual3D
                {
                    Content = model
                };
        
                // Add the 3D model to HelixViewport3D
                HelixViewport3D.Children.Add(modelVisual);
            }
            catch (Exception ex)
            {
                // Handle errors during loading (e.g., invalid file format)
                System.Windows.MessageBox.Show($"Error loading model: {ex.Message}", "Error", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
            }
        }
    }
}