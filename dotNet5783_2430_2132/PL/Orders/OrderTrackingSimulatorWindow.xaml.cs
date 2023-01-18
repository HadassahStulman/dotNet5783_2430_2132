using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.ComponentModel;
using PL;
using System.Threading;

namespace PL.Orders;

/// <summary>
/// Interaction logic for OrderTrackingSimulatorWindow.xaml
/// </summary>
public partial class OrderTrackingSimulatorWindow : Window
{
    private static BlApi.IBl bl = BlApi.Factory.Get();
    private ObservableCollection<BO.OrderForList> OrderList;
    BackgroundWorker BWOrder;
    private DateTime currentTime = DateTime.Now;
    public OrderTrackingSimulatorWindow()
    {
        try
        {
            InitializeComponent();
            OrderList = new ObservableCollection<BO.OrderForList>(bl.Order.GetList()!);
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
    private void TrackOrderButton_click(object sender, RoutedEventArgs e)
    {
        try
        {
            BO.OrderForList? order = (sender as Button)!.DataContext as BO.OrderForList;
            new TrackOrderWindow(order?.ID ?? throw new ArgumentNullException()).ShowDialog();
        }
        catch (Exception ex) { MessageBox.Show(ex.Message, "Exception Thrown"); }
    }

    private void BWOrder_DoWork(object sender, DoWorkEventArgs e)
    {
        for (int i = 0; i < OrderList.Count; i++)
        {
            if (BWOrder.CancellationPending == true)
            {
                e.Cancel = true;
                break;

            }
            else
            {
                Thread.Sleep(2000);
                if (BWOrder.WorkerReportsProgress == true)
                {
                    currentTime.AddHours(8);
                    //BWOrder.ReportProgress();
                }


            }

        }
    }

    private void BWOrder_ProgressChanged(object sender, ProgressChangedEventArgs e)
    {
        for (int i = 0; i < OrderList.Count; i++)
        {

        }
    }

    private void BWOrder_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
    {
        if (e.Cancelled == true)
        {
            MessageBox.Show("order updates have been cancelled, please continue later");
        }
        else
        {
            //e.Result
            MessageBox.Show("all updates have been successfully completed!");
        }

    }

    private void startButton_Click(object sender, RoutedEventArgs e)
    {
        if (BWOrder.IsBusy != true)
            BWOrder.RunWorkerAsync(35);
    }

    private void stopButton_Click(object sender, RoutedEventArgs e)
    {
        if (BWOrder.WorkerSupportsCancellation == true)
            BWOrder.CancelAsync();
    }
}
