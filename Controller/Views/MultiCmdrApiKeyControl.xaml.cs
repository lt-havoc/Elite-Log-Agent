#nullable enable
namespace DW.ELA.Controller.Views
{
    using System;
    using System.Linq;
    using Avalonia.Controls;
    using Avalonia.Interactivity;
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
        
        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        protected override void OnDataContextChanged(EventArgs e)
        {
            base.OnDataContextChanged(e);

            if (DataContext is MultiCmdrApiKeyViewModel context)
                context.ApiKeyAdded += OnApiKeyAdded;                
        }

        private void ApiKeyGrid_DoubleTapped(object sender, RoutedEventArgs e)
        {
            var dataGrid = (DataGrid)sender;
            dataGrid.IsReadOnly = false;
            dataGrid.BeginEdit();
        }
            
        private void ApiKeyGrid_RowEditEnded(object sender, DataGridRowEditEndedEventArgs e)
        {
            var dataGrid = this.FindControl<DataGrid>("ApiKeyGrid");
            dataGrid.IsReadOnly = true;
            dataGrid.SelectedItem = null;
            
            // Current column isn't reset after editing a row. The first time adding a new row focuses the cmdr name
            // column text box. If the row editing ends with the api key column as the last text box that was
            // focused, the subsequent added row will focus on the api key column instead of cmdr name. Resetting
            // the current column fixes that.
            dataGrid.CurrentColumn = dataGrid.Columns.First();
        }

        private void OnApiKeyAdded(object sender, ApiKeyAddedEventArgs e)
        {
            var dataGrid = this.FindControl<DataGrid>("ApiKeyGrid");
            dataGrid.SelectedItem = e.ApiKeyViewModel;
            dataGrid.IsReadOnly = false;
            dataGrid.BeginEdit();
            var visual = dataGrid.GetVisualDescendants().FirstOrDefault(v => v is DataGridCell c && c.Content is TextBox);
            
            if (visual is DataGridCell cell && cell.Content is TextBox input)
                input.Focus();
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