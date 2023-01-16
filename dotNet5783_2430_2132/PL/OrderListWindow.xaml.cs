
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;


namespace PL;

/// <summary>
/// Interaction logic for OrderListWindow.xaml
/// </summary>
public partial class OrderListWindow : Window
{
    private BlApi.IBl bl = BlApi.Factory.Get();
    private ObservableCollection<BO.OrderForList?> myOrderCollection;
    /// <summary>
    ///  constructer
    /// </summary>
    public OrderListWindow()
    {
        InitializeComponent();
        myOrderCollection = new (bl.Order.GetList());
        OrderListView.DataContext = myOrderCollection;
        
    }

    /// <summary>
    /// opening a eindow with details of an order
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void OrderDoubleClick(object sender, MouseButtonEventArgs e)
    {
        try
        {
            BO.OrderForList o = (OrderListView.SelectedItem as BO.OrderForList) ?? throw new NullReferenceException();
            new Orders.OrderWindow(o.ID, "manager").ShowDialog();

            myOrderCollection = new(bl.Order.GetList());
            OrderListView.DataContext = myOrderCollection;

        }
        catch (Exception )
        {
            MessageBox.Show("please select an order");
        }
    }

    private void Back_Button_Click(object sender, RoutedEventArgs e) => Close();
}



