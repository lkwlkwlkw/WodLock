using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Threading;
using Microsoft.Extensions.Configuration;
using System;
using System.Diagnostics;

namespace WodLock.Views;

public partial class MainWindow : Window
{
    private readonly HttpServer server = new();
    private GPIO? IO;
    private bool PlatformWithGPIO;
    private int LockPin;
    private int DoorOpenedPin;
    private int BuzzerPin;
    uint NumberWaterSold;

    public MainWindow()
    {
        InitializeComponent();
    }

    private void Window_Loaded(object sender, RoutedEventArgs e)
    {
        ReadINI();
        server.Start();
        server.WaterPurchased+= WaterPurchased; //delegat do zdarzenia
        try
        {
            IO = new GPIO(LockPin, DoorOpenedPin,BuzzerPin);
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            PlatformWithGPIO = false;
        }
    }

    private void WaterPurchased(object? sender, EventArgs? e)
    {
        NumberWaterSold++;
        Debug.WriteLine (NumberWaterSold.ToString ());
        if (PlatformWithGPIO ) //otwarcie zamka
        {
            Dispatcher.UIThread.Post(() => IO?.OpenDoor());
        }
    }
    private void ReadINI()
    {
        var configuration = new ConfigurationBuilder()
            .AddIniFile("appsettings.ini")
            .AddEnvironmentVariables()
            .Build();


        LockPin = configuration.GetValue<Int16>("LockPin");
        DoorOpenedPin = configuration.GetValue<Int16>("DoorOpenedPin");
        BuzzerPin = configuration.GetValue<Int16>("BuzzerPin");
    }

}
