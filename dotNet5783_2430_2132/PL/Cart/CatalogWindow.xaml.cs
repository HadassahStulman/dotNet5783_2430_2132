using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;


namespace PL.Cart
{
    /// <summary>
    /// Interaction logic for CatalogWindow.xaml
    /// </summary>
    public partial class CatalogWindow : Window
    {
        private static BlApi.IBl bl = BlApi.Factory.Get();
        private IEnumerable<ProductForList?> products = bl.Product.GetAll();
        private BO.Cart cart;
        public CatalogWindow( ref BO.Cart cart)
        {
            InitializeComponent();
            this.cart = cart;
            this.DataContext = products;
            CategorySelector.ItemsSource = CategorySelector.ItemsSource = Enum.GetValues(typeof(PL.Products.Enums.Category));
        }

        private void CategorySelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ProductListView.ItemsSource = from product in products
                                          let choice = CategorySelector.SelectedItem.ToString()
                                          where choice == PL.Products.Enums.Category.All.ToString() ? true : product.Category.ToString() == choice
                                          select product;
        }

        private void ChooseItem_DoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            try
            {
                BO.ProductForList? product = ProductListView.SelectedItem as ProductForList ?? throw new ArgumentNullException();
                new ProductItemWindow(ref cart, product.ID, "ADD").ShowDialog();
            }
            catch
            {
                MessageBox.Show("Please Choose a Product");
            }
        }

        private void GoToCartButton_Click(object sender, RoutedEventArgs e)
        {
            new CartWindow(ref cart).Show();
            Close();
        }
    }
}
