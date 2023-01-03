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
    public CartWindow(ref BO.Cart cart)
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

    private void ItemListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
    {
        try
        {
            int id = (ItemListView.SelectedItem as BO.OrderItem)?.ProductID ?? throw new ArgumentNullException();
            new ProductItemWindow(ref cart, id, "UPDATE").ShowDialog();
        }
        catch (Exception)
        {
            MessageBox.Show("Please Choose a Item", "Exception Thrown");
        }
    }

    private void ContinueShopping_Click(object sender, RoutedEventArgs e)
    {
        new CatalogWindow(ref cart).Show();
        Close();
    }

    private void ConfirmOrder_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            int orderID = bl.Cart.OrderCart(cart);
            MessageBox.Show($"Order ID is {orderID}", "successfully Oredered");
            Close();
        }
        catch (Exception ex) { MessageBox.Show(ex.ToString(), "Exception Thrown"); }
    }
}
