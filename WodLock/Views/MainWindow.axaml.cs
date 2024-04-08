using Avalonia.Controls;
using Avalonia.Interactivity;

namespace WodLock.Views;

public partial class MainWindow : Window
{
    HttpServer server = new HttpServer();

    public MainWindow()
    {
        InitializeComponent();
    }

    private void Window_Loaded(object sender, RoutedEventArgs e)
    {
        server.Start();
    
    }
}
