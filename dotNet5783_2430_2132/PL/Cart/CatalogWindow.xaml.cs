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
        private IEnumerable<ProductForList?> products;
        private BO.Cart cart;
    

        /// <summary>
        /// catalog constructor
        /// </summary>
        /// <param name="cart"></param>
        public CatalogWindow(BO.Cart cart)
        {
            try
            {
                InitializeComponent();
                products = bl.Product.GetAll();
                this.cart = cart;
                this.DataContext = products;
                CategorySelector.ItemsSource = CategorySelector.ItemsSource = Enum.GetValues(typeof(PL.Products.Enums.Category));
            }
            catch(Exception ex) { MessageBox.Show(ex.ToString(), "Exception Trown"); }
        }

        /// <summary>
        /// select a category of products to view in list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CategorySelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                ProductListView.ItemsSource = from product in products
                                              let choice = CategorySelector.SelectedItem.ToString()
                                              where choice == PL.Products.Enums.Category.All.ToString() ? true : product.Category.ToString() == choice
                                              select product;
            }
            catch (Exception ex) { MessageBox.Show( ex.ToString(), "Exception Trown"); }
        }


        /// <summary>
        /// event handler for choosing a product to add to cart
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChooseItem_DoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            try
            {
                BO.ProductForList? product = ProductListView.SelectedItem as ProductForList ?? throw new ArgumentNullException();
                new ProductItemWindow(cart, product.ID, "ADD").ShowDialog(); // open product window on adding mode
            }
            catch
            {
                MessageBox.Show("Please Choose a Product");
            }
        }


        /// <summary>
        /// open cart display window 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GoToCartButton_Click(object sender, RoutedEventArgs e)
        {
            new CartWindow(cart).ShowDialog();
            Close();
        }
    }
}
