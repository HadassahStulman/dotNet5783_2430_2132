using System;
using System.Collections.ObjectModel;
using System.Windows;

using System.Windows.Input;


namespace PL.Cart;

/// <summary>
/// Interaction logic for CartWindow.xaml
/// </summary>
public partial class CartWindow : Window
{
    private BlApi.IBl bl = BlApi.Factory.Get();
    private BO.Cart cart;

    /// <summary>
    /// window constructor
    /// </summary>
    /// <param name="cart"></param>
    public CartWindow(BO.Cart cart)
    {
        try
        {
            InitializeComponent();
            this.cart = cart;
            this.DataContext = this.cart;
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
    }


    /// <summary>
    /// double click on pruduct in catalog event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void ItemListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
    {
        try
        {
            int id = (ItemListView.SelectedItem as BO.OrderItem)?.ProductID ?? throw new ArgumentNullException();
            new ProductItemWindow(cart, id, "UPDATE").ShowDialog(); // open product view window
        }
        catch (Exception)
        {
            MessageBox.Show("Please Choose a Item", "Exception Thrown");
        }
    }

    /// <summary>
    /// back to catalog
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void ContinueShopping_Click(object sender, RoutedEventArgs e)
    {
        new CatalogWindow(cart).Show();
        Close();
    }

    /// <summary>
    /// finish shopong and place order
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void ConfirmOrder_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            int orderID = bl.Cart.OrderCart(cart); // confirm order
            MessageBox.Show($"Order ID is {orderID}", "successfully Oredered");  // return order ID
            Close();
        }
        catch (Exception ex) { MessageBox.Show(ex.ToString(), "Exception Thrown"); }
    }
}
