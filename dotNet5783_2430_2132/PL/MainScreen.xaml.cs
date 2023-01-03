
using System.Windows;
using System.Windows.Controls;
using PL.Orders;


namespace PL;

/// <summary>
/// Interaction logic for MainScreen.xaml
/// </summary>
public partial class MainScreen : Window
{
    /// <summary>
    /// constructer
    /// </summary>
    public MainScreen()
    {
        InitializeComponent();
    }
    /// <summary>
    /// opening a window with operations for manager
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void ManagerDisplayButton_Click(object sender, RoutedEventArgs e) => new ManagerDisplayWindow().ShowDialog();

    private void NewOrderButton_Click(object sender, RoutedEventArgs e) => new OrderWindow().ShowDialog();

    /// <summary>
    /// opening a window that tracks a specific order
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void TrackOrderButton_Click(object sender, RoutedEventArgs e) => new TrackOrderDiplayWindow().ShowDialog();
}
