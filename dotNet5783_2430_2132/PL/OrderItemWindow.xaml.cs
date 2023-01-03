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
    int oID = 0;

    /// <summary>
    /// constructer
    /// </summary>
    /// <param name="source"></param>
    /// <param name="id"></param>
    /// <param name="oi"></param>
    public OrderItemWindow(string source, string orderStatus, int id, BO.OrderItem oi)
    {
        InitializeComponent();

        myOrderItem = oi;
        this.DataContext = myOrderItem;
        oID = id;

        if (source == "manager" && orderStatus == "OrderConfirmed") 
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
        try {

            bl.Order.ManagerUpdateOrder(oID, myOrderItem.ProductID, myOrderItem.Amount);
            myOrderItem.TotalPrice = myOrderItem.Amount * myOrderItem.Price;
            MessageBox.Show("The order has been successfully updated!");
        }
        catch(Exception ex) {
            MessageBox.Show( ex.ToString()); 
        }

    }

    private void Back_Button_Click(object sender, RoutedEventArgs e)=> Close();
}
