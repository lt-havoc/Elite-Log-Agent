namespace DW.ELA.Controller
{
    using System;
    using Avalonia.Controls;
    using Avalonia.Markup.Xaml;

    public class MultiCmdrApiKeyControl : UserControl
    {
        public static readonly Type View = typeof(MultiCmdrApiKeyControl);
        
        public MultiCmdrApiKeyControl()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}