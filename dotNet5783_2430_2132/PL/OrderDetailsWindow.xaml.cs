using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using BO;

namespace PL;

/// <summary>
/// Interaction logic for OrderDetailsWindow.xaml
/// </summary>
public partial class OrderDetailsWindow : Window
{
    /// <summary>
    /// private feiled for accessing to bl methods
    /// </summary>
    private readonly BlApi.IBl bl = BlApi.Factory.Get();
    /*PL*/BO.Order myOrder;
    ObservableCollection<BO.OrderItem?>? myOrderItem = null;
    string oiSource;




    /// <summary>
    /// constructer
    /// </summary>
    /// <param name="id"></param>
    /// <param name="source"></param>
    public OrderDetailsWindow(int id,string source)
    {
        try
        {
            InitializeComponent();
            myOrder = bl.Order.GetByID(id);
            this.DataContext = myOrder;
            Status_ComboBox.ItemsSource = Enum.GetValues(typeof(BO.Enums.OrderStatus)); // initialize order status combobox
            
            myOrderItem = new(myOrder.Items!.ToList()!);
            OrderItems_ListBox.DataContext = myOrderItem;

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
        }catch(Exception ex) { MessageBox.Show(ex.Message); }

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
        catch(Exception ex) { MessageBox.Show(ex.ToString()); }
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
            new OrderItemWindow(oiSource ,myOrder.Status.ToString(), myOrder.ID, oi).ShowDialog();

            /////////////////////////////
            BO.Order? newOrder = bl.Order.GetByID(myOrder.ID);
            this.DataContext = newOrder;

            myOrderItem = new(myOrder.Items!.ToList()!);
            OrderItems_ListBox.DataContext = myOrderItem;

        }
        catch (Exception ex) { MessageBox.Show(ex.ToString()); }
    }

    private void Back_Button_Click(object sender, RoutedEventArgs e) => Close();

}
