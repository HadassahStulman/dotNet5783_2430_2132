using PL.Products;
using System.Windows;

namespace PL;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    /// <summary>
    /// constructer
    /// </summary>
    public MainWindow()
    {
        InitializeComponent();
    }

    /// <summary>
    /// opening a window for products display
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void ShowProductListButton_Click(object sender, RoutedEventArgs e) => new ProductForListWindow().Show();

}
