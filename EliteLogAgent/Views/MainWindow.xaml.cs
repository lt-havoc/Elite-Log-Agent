using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace EliteLogAgent.Views;

public class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
#if DEBUG
        this.AttachDevTools();
#endif
        AvaloniaXamlLoader.Load(this);
    }
}