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
    public ManagerDisplayWindow()
    {
        InitializeComponent();
    }

    private void ProductForListDisplay_Click(object sender, RoutedEventArgs e) => new ProductForListWindow().Show();
}
