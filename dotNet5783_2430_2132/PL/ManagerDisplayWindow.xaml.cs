using System.Windows.Controls;
using System.Windows;
using PL.Products;
using PL.Orders;

namespace PL;

/// <summary>
/// Interaction logic for ManagerDisplayWindow.xaml
/// </summary>
public partial class ManagerDisplayWindow : Window
{
    /// <summary>
    /// constructer
    /// </summary>
    public ManagerDisplayWindow()
    {
        InitializeComponent();
    }

    /// <summary>
    /// opening a window for products display
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void ProductForListDisplay_Click(object sender, RoutedEventArgs e) => new ProductForListWindow().Show();

    /// <summary>
    /// opening a window for order display
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void OrderForListDisplay_Click(object sender, RoutedEventArgs e)=>new OrderListWindow().ShowDialog();

    private void Back_Button_Click(object sender, RoutedEventArgs e) => Close();
    
}
