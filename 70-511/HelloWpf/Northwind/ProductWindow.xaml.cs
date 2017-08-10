using System;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

using Northwind.Data.Entities;

namespace HelloWpf.Northwind
{
    /// <summary>
    /// Interaction logic for ProductWindow.xaml
    /// </summary>
    public partial class ProductWindow : Window
    {
        #region Private Fields

        private ICollectionView _collectionView;
        private int _productCount;
        private string _filterText;

        #endregion

        #region Constructor

        public ProductWindow()
        {
            InitializeComponent();
        }

        #endregion

        #region Event Handlers

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            CollectionViewSource productViewSource  = (CollectionViewSource)FindResource("ProductViewSource");
            InitializeView(productViewSource);

            MinHeight = MaxHeight = ActualHeight;
        }

        private void ButtonBack_Click(object sender, RoutedEventArgs e)
        {
            _collectionView.MoveCurrentToPrevious();
            UpdateNavigationControls();
        }

        private void ButtonForward_Click(object sender, RoutedEventArgs e)
        {
            _collectionView.MoveCurrentToNext();
            UpdateNavigationControls();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            _filterText = ((TextBox)e.OriginalSource).Text;
            _collectionView.Refresh();
            _productCount = ((CollectionView)(_collectionView)).Count;
            UpdateNavigationControls();
        }

        #endregion

        #region Implementation

        private void InitializeView(CollectionViewSource productViewSource)
        {
            string connectionString = ConfigurationManager.ConnectionStrings[ConstantsHelper.NORTHWIND_ENTITIES_CONNECTION_STRING].ConnectionString;
            NorthwindObjectContext context = new NorthwindObjectContext(connectionString);

            // Default is BindingListCollectionView.
            // ToList() switches to ListCollectionView that provides richer functionality
            productViewSource.Source = context.Products.ToList();
            productViewSource.Filter += FilterProducts;

            _collectionView = productViewSource.View;
            _productCount = ((CollectionView)(_collectionView)).Count;
            UpdateNavigationControls();
        }

        private void FilterProducts(object sender, FilterEventArgs e)
        {
            e.Accepted = string.IsNullOrEmpty(_filterText) ||
                ((Product)e.Item).ProductName.IndexOf(_filterText, StringComparison.OrdinalIgnoreCase) != -1;
        }

        private void UpdateNavigationControls()
        {
            NavigationLabel.Text =
                $"Record {_collectionView.CurrentPosition + 1} of {_productCount}";

            ButtonBack.IsEnabled = (_collectionView.CurrentPosition > 0);
            ButtonForward.IsEnabled = (_collectionView.CurrentPosition < _productCount - 1);
        }

        #endregion
    }
}
