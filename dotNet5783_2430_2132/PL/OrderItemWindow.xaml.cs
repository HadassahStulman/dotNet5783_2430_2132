using System;
using System.Windows;
namespace PL;

/// <summary>
/// Interaction logic for OrderItemWindow.xaml
/// </summary>
public partial class OrderItemWindow : Window
{
    /// <summary>
    /// private feiled for accessing to bl methods
    /// </summary>
    private readonly BlApi.IBl bl = BlApi.Factory.Get();
    private BO.OrderItem myOrderItem;
    BO.Order myOrder;

    /// <summary>
    /// constructer
    /// </summary>
    /// <param name="source"></param>
    /// <param name="id"></param>
    /// <param name="oi"></param>
    public OrderItemWindow(string source, BO.Order orderToUpdate, BO.OrderItem oi)
    {
        InitializeComponent();

        myOrderItem = oi;
        this.DataContext = myOrderItem;
        this.myOrder = orderToUpdate;

        if (source == "manager" && orderToUpdate.Status.ToString() == "OrderConfirmed")
        {

            UpdateAmount_Button.Visibility = Visibility.Visible;
            UpdateAmount_Button.IsEnabled = true;
            Amount_TextBox.IsReadOnly = false;
        }

    }

    /// <summary>
    /// updating amount of products FOR MANAGER
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void UpdateAmount_Button_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            BO.Order newOrder=bl.Order.ManagerUpdateOrder(myOrder.ID, myOrderItem.ProductID, myOrderItem.Amount)!;
            myOrder.TotalPrice = newOrder.TotalPrice;
            myOrder.Items= newOrder.Items;
            myOrderItem.TotalPrice = myOrderItem.Amount * myOrderItem.Price;
            MessageBox.Show("The order has been successfully updated!");
            Close();
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.ToString());
        }

    }

    private void Back_Button_Click(object sender, RoutedEventArgs e) => Close();
}
