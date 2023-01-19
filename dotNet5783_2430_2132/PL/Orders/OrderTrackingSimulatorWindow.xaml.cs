using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.ComponentModel;
using PL;
using System.Threading;
using System.Linq;

namespace PL.Orders;

/// <summary>
/// Interaction logic for OrderTrackingSimulatorWindow.xaml
/// </summary>
public partial class OrderTrackingSimulatorWindow : Window
{
    private static BlApi.IBl bl = BlApi.Factory.Get();
    private ObservableCollection</*PL.Orders.*/BO.OrderForList> OrderList;
    BackgroundWorker BWOrder;
    public OrderTrackingSimulatorWindow()
    {
        try
        {
            InitializeComponent();
            OrderList = new(bl.Order.GetList()!);
            this.DataContext = OrderList;

            BWOrder = new BackgroundWorker();
            BWOrder.DoWork += BWOrder_DoWork; 
            BWOrder.ProgressChanged += BWOrder_ProgressChanged;
            BWOrder.RunWorkerCompleted += BWOrder_RunWorkerCompleted;

            BWOrder.WorkerReportsProgress = true;
            BWOrder.WorkerSupportsCancellation = true;
        }
        catch (Exception ex) { MessageBox.Show(ex.Message, "Exception Thrown"); }
    }

    /// <summary>
    /// open window with track info for each order
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void TrackOrderButton_click(object sender, RoutedEventArgs e)
    {
        try
        {
            BO.OrderForList? order = (sender as Button)!.DataContext as /*PL.Orders.*/BO.OrderForList;
            new TrackOrderWindow(order?.ID ?? throw new ArgumentNullException()).Show();
        }
        catch (Exception ex) { MessageBox.Show(ex.Message, "Exception Thrown"); }
    }

    /// <summary>
    /// begining of background worker
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void BWOrder_DoWork(object sender, DoWorkEventArgs e)
    {
        try
        {
            Random rnd = new Random();
            int manageTime;
            while (BWOrder.CancellationPending != true) // while backgroung worker was not cancled
            {
                BO.Order? orderToManage = bl.Order.NextOrderToManage(); // getting an order to manage
                if (orderToManage == null)
                    Thread.Sleep(2000);
                else
                {
                    manageTime = rnd.Next(3, 11); // random time 
                    Thread.Sleep(manageTime * 1000);
                    if (BWOrder.WorkerReportsProgress == true)
                    {
                        BWOrder.ReportProgress(manageTime, orderToManage);
                    }
                }
                Thread.Sleep(1000);
            }
            e.Cancel = true;
        }
        catch (Exception ex) { MessageBox.Show(ex.ToString(), "Exception Thrown"); }
    }

    /// <summary>
    /// changes the Order Status and the UI
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void BWOrder_ProgressChanged(object sender, ProgressChangedEventArgs e)
    {
        try
        {
            int index;
            BO.Order orderToManage = e.UserState as BO.Order ?? throw new NullReferenceException();
            if (orderToManage.Status == BO.Enums.OrderStatus.OrderConfirmed)
            {
                index = OrderList.IndexOf(OrderList.Where(order => order.ID == orderToManage.ID).First());
                BO.Order UpdatedOrder = bl.Order.UpdateShipping(orderToManage.ID);
                OrderList[index] = new BO.OrderForList
                {
                    ID = orderToManage.ID,
                    CustomerName = orderToManage.CustomerName,
                    AmountOfItems = orderToManage.Items!.Count(),
                    Status = BO.Enums.OrderStatus.OrderShipped,
                    TotalPrice = orderToManage.TotalPrice,
                };
            }            
            else if (orderToManage.Status == BO.Enums.OrderStatus.OrderShipped)
            {
                index = OrderList.IndexOf(OrderList.Where(order => order.ID == orderToManage.ID).First());
                BO.Order UpdatedOrder = bl.Order.UpdateDelivery(orderToManage.ID);
                OrderList[index] = new BO.OrderForList
                {
                    ID = orderToManage.ID,
                    CustomerName = orderToManage.CustomerName,
                    AmountOfItems = orderToManage.Items!.Count(),
                    Status = BO.Enums.OrderStatus.OrderDelivered,
                    TotalPrice = orderToManage.TotalPrice,
                };
            }
        }
        catch (Exception ex) { MessageBox.Show(ex.ToString(), "Exception Thrown"); }
    }

    /// <summary>
    /// end of background worker
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void BWOrder_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
    {
        if (e.Cancelled == true)
        {
            MessageBox.Show("order updates have been cancelled, please continue later");
        }
        else
        {
            MessageBox.Show("all updates have been successfully completed!");
        }

    }

    private void startButton_Click(object sender, RoutedEventArgs e)
    {
        if (BWOrder.IsBusy != true)
            BWOrder.RunWorkerAsync();
    }

    private void stopButton_Click(object sender, RoutedEventArgs e)
    {
        if (BWOrder.WorkerSupportsCancellation == true)
            BWOrder.CancelAsync();
    }

    private void BackButton_Click(object sender, RoutedEventArgs e)
    {
        stopButton_Click(sender, e);
        Close();
    }
}