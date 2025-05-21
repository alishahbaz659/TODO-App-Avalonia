using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Microsoft.Extensions.DependencyInjection;
using TodoApp.Interfaces;
using TodoApp.Services;
using TodoApp.ViewModels;
using TodoApp.Views;
using Avalonia.Media.Imaging;
using System;
using Avalonia.Controls;
using System.IO;
using System.Runtime.InteropServices;

namespace TodoApp;

public partial class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            var services = new ServiceCollection();
            ConfigureServices(services);
            var serviceProvider = services.BuildServiceProvider();

            var mainWindow = new MainWindow
            {
                DataContext = serviceProvider.GetRequiredService<MainViewModel>()
            };

            // Set window icon with platform-specific handling
            try
            {
                // Determine the correct icon path based on platform
                string iconPath;
                if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                {
                    // For macOS, use absolute path
                    string baseDir = AppContext.BaseDirectory;
                    iconPath = Path.Combine(baseDir, "Assets", "app_icon.png");
                    
                    // If that doesn't exist (when running from IDE), try the project directory
                    if (!File.Exists(iconPath))
                    {
                        // Try to find the assets directory relative to current directory
                        string currentDir = Directory.GetCurrentDirectory();
                        iconPath = Path.Combine(currentDir, "Assets", "app_icon.png");
                    }
                }
                else
                {
                    // For Windows and Linux, this relative path should work
                    iconPath = "Assets/app_icon.png";
                }

                if (File.Exists(iconPath))
                {
                    mainWindow.Icon = new WindowIcon(new Bitmap(iconPath));
                    System.Diagnostics.Debug.WriteLine($"Successfully loaded icon from: {iconPath}");
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine($"Icon file not found at: {iconPath}");
                }
            }
            catch (Exception ex)
            {
                // Log error - icon couldn't be loaded
                System.Diagnostics.Debug.WriteLine($"Failed to load application icon: {ex.Message}");
            }

            desktop.MainWindow = mainWindow;
        }

        base.OnFrameworkInitializationCompleted();
    }

    private void ConfigureServices(IServiceCollection services)
    {
        services.AddSingleton<ITodoService, TodoService>();
        services.AddTransient<MainViewModel>();
    }
}