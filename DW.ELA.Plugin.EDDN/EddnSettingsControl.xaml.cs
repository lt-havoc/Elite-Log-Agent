namespace DW.ELA.Plugin.EDDN
{
    using Avalonia.Controls;
    using Avalonia.Markup.Xaml;
    
    public class EddnSettingsControl : UserControl
    {
        public EddnSettingsControl()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}