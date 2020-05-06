namespace EliteLogAgent.Views
{
    using Avalonia.Controls;
    using Avalonia.Markup.Xaml;

    public class GeneralSettingsView : UserControl
    {        
        public GeneralSettingsView()
        {
            InitializeComponent();
        }
        
        private void InitializeComponent() => AvaloniaXamlLoader.Load(this);
    }
}