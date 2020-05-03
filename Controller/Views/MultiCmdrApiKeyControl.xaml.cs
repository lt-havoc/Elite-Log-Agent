#nullable enable
namespace DW.ELA.Controller.Views
{
    using System;
    using System.Linq;
    using Avalonia.Controls;
    using Avalonia.Markup.Xaml;
    using Avalonia.VisualTree;
    using ViewModels;

    public class MultiCmdrApiKeyControl : UserControl
    {
        public static readonly Type View = typeof(MultiCmdrApiKeyControl);
        
        public MultiCmdrApiKeyControl()
        {
            InitializeComponent();
            
        }

        protected override void OnDataContextChanged(EventArgs e)
        {
            base.OnDataContextChanged(e);

            if (DataContext is MultiCmdrApiKeyViewModel context)
                context.ApiKeyAdded += OnApiKeyAdded;                
        }

        private void OnApiKeyAdded(object sender, ApiKeyAddedEventArgs e)
        {
            var dataGrid = this.FindControl<DataGrid>("ApiKeyGrid");
            dataGrid.SelectedItem = e.ApiKeyViewModel;
            var cell = dataGrid.GetVisualDescendants()
                               .Where(d => d is DataGridCell)
                               .Reverse()
                               .Skip(1) // Don't know what this cell is
                               .Skip(dataGrid.Columns.Count - 1) // Skip 2nd and 3rd columns
                               .Take(1) // 1st column - CMDR Name
                               .SingleOrDefault();

            if (cell is DataGridCell c)
            {
                dataGrid.BeginEdit();
                ((TextBox)c.Content).Focus();
            }
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        private void ApiKeyGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var viewModel = DataContext as MultiCmdrApiKeyViewModel ?? throw new ArgumentNullException(nameof(DataContext));
            var grid = sender as DataGrid ?? throw new ArgumentNullException(nameof(sender));
            
            var selectedObjects = grid.SelectedItems.OfType<ApiKeyViewModel>().ToArray();

            viewModel.SelectedItems = selectedObjects;
        }
    }
}