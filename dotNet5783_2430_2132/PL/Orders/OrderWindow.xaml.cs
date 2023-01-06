using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PL.Orders;

/// <summary>
/// Interaction logic for OrderWindow.xaml
/// </summary>
/// <summary>
/// Interaction logic for OrdersWindow.xaml
/// </summary>
public partial class OrderWindow : Window
{
    /// <summary>
    /// private feiled for accessing to bl methods
    /// </summary>
    private readonly BlApi.IBl bl = BlApi.Factory.Get();
    private BO.Order myOrder;
    string oiSource;




    /// <summary>
    /// constructer
    /// </summary>
    /// <param name="id"></param>
    /// <param name="source"></param>
    public OrderWindow(int id, string source)
    {
        try
        {
            InitializeComponent();
            myOrder = bl.Order.GetByID(id);
            this.DataContext = myOrder;
            Status_ComboBox.ItemsSource = Enum.GetValues(typeof(BO.Enums.OrderStatus)); // initialize order status combobox

            if (source == "manager")
            {
                oiSource = "manager";
                if (myOrder.Status == BO.Enums.OrderStatus.OrderConfirmed)
                {
                    Ship_Button.Visibility = Visibility.Visible;
                    Ship_Button.IsEnabled = true;
                }
                if (myOrder.Status == BO.Enums.OrderStatus.OrderShipped)
                {
                    Deliver_Button.Visibility = Visibility.Visible;
                    Deliver_Button.IsEnabled = true;
                }

            }
            else oiSource = "customer";
        }
        catch (Exception ex) { MessageBox.Show(ex.Message); }

    }



    /// <summary>
    /// shipping an order
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void Ship_Button_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            bl.Order.UpdateShipping(myOrder.ID);
            myOrder.Status = BO.Enums.OrderStatus.OrderShipped;
            myOrder.ShipDate = DateTime.Now;

            Ship_Button.Visibility = Visibility.Hidden;
            Ship_Button.IsEnabled = false;
            Deliver_Button.Visibility = Visibility.Visible;
            Deliver_Button.IsEnabled = true;
            MessageBox.Show("The order has been successfully shipped!");

        }
        catch (Exception ex) { MessageBox.Show(ex.ToString()); }
    }

    /// <summary>
    /// delivering an order
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void Deliver_Button_Click(object sender, RoutedEventArgs e)
    {
        try
        {

            bl.Order.UpdateDelivery(myOrder.ID);
            myOrder.DeliveryDate = DateTime.Now;
            myOrder.Status = BO.Enums.OrderStatus.OrderDelivered;

            Deliver_Button.Visibility = Visibility.Hidden;
            Deliver_Button.IsEnabled = false;
            MessageBox.Show("The order has been successfully delivered!");

        }
        catch (Exception ex) { MessageBox.Show(ex.ToString()); }
    }


    /// <summary>
    /// opening a window of a specific ordr item
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void OrderItemDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
    {
        try
        {
            BO.OrderItem oi = (OrderItems_ListBox.SelectedItem as BO.OrderItem) ?? throw new NullReferenceException();
            new OrderItemWindow(oiSource, myOrder, oi, updateAmount).ShowDialog();
        }
        catch (Exception ex) { MessageBox.Show(ex.ToString()); }
    }

    private void Back_Button_Click(object sender, RoutedEventArgs e) => Close();


    /// <summary>
    /// action that can change total price of order from other windows
    /// </summary>
    /// <param name="order"></param>
    private void updateAmount(BO.Order order)
    {
        myOrder.TotalPrice = order.TotalPrice;
        myOrder.Items= order.Items;
    }
}