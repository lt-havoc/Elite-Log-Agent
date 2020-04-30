namespace DW.ELA.Controller
{
    using System;
    using System.Linq;
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

        private void ApiKeyGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var viewModel = DataContext as MultiCmdrApiKeyViewModel ?? throw new ArgumentNullException(nameof(DataContext));
            var grid = sender as DataGrid ?? throw new ArgumentNullException(nameof(sender));
            
            var selectedObjects = grid.SelectedItems.OfType<ApiKey>().ToArray();

            viewModel.SelectedItems = selectedObjects;
        }
    }
}