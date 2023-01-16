
using System;
using System.Windows;


namespace PL.Orders;

/// <summary>
/// Interaction logic for TrackOrderWindow.xaml
/// </summary>
public partial class TrackOrderWindow: Window
{
    /// <summary>
    /// private feiled for accessing to bl methods
    /// </summary>
    private readonly BlApi.IBl bl = BlApi.Factory.Get();
    BO.Order? myOrder = null;
    /// <summary>
    /// constructer
    /// </summary>
    /// <param name="id"></param>
    public TrackOrderWindow(int id)
    {
        try
        {
            InitializeComponent();
            myOrder = bl.Order.GetByID(id);
            this.DataContext = myOrder;
            OrderStages_ListBox.DataContext = bl.Order.TrackOrder(id).TrackingStages; // difining the source of data to be the list of tracking stagese
        }
        catch(Exception ex) { MessageBox.Show(ex.ToString(), "Exception Thrown"); }
    }

    /// <summary>
    /// go back to previous window
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void Back_button_Click(object sender, RoutedEventArgs e) => Close();
}
